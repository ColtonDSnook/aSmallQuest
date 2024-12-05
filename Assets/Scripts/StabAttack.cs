using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StabAttack : Ability
{
    public float baseDamage;
    public SpinAttack spinAttack;
    public bool selectionMade = false;
    public Combatant selection;
    public bool timersPaused = false;
    public GameObject selectText;

    public void Update()
    {
        baseDamage = gameManager.stabDamage;

        if (timeRemaining > 0)
        {
            if (!timersPaused)
            {
                if (timeRemaining < 1)
                {
                    timerText.text = timeRemaining.ToString("N1");
                    timeRemaining -= Time.deltaTime;
                    abilityRadial.fillAmount = timeRemaining / maxCountDownTime;
                }
                else
                {
                    timerText.text = timeRemaining.ToString("N0");
                    timeRemaining -= Time.deltaTime;
                    abilityRadial.fillAmount = timeRemaining / maxCountDownTime;
                }
            }
        }
        else
        {
            RefreshAbility();
        }
    }

    public override void UseAbility()
    {
        if (timeRemaining == 0)
        {
            if (combatManager.combatState == CombatManager.CombatState.InCombat && isActive)
            {
                ability.sprite = abilityGrey;
                abilityRadial.fillAmount = 1; // this can stay as long as the actual cooldown is paused until the end of the attack
                combatManager.selection = true;
            }
            else
            {
                Debug.Log("Cannot use ability while dead/dying");
                return;
            }
        }
        else
        {
            Debug.Log("Wait for cooldown to finish");
            return;
        }

        if (timeRemaining > 0)
        {
            Debug.Log("Cannot use ability. Please wait for cooldown");
            return;
        }

        StartCoroutine(WaitForSelection());

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
    }

    public IEnumerator Stab(Combatant target)
    {
        selectText.SetActive(false);
        animator.Play("MC_Stab");
        player.PauseTimer();
        spinAttack.isActive = false;
        yield return new WaitForSeconds(0.5f);
        float damage = target.healthSystem.TakeDamage(gameManager.damage * baseDamage);
        player.healthSystem.Heal((float)Math.Round(damage * gameManager.healing));
        foreach (Combatant combatant in combatManager.combatants)
        {
            combatant.UnpauseTimer();
        }
        spinAttack.isActive = true;
        selection = null;
        combatManager.selection = false;
        selectionMade = false;
        spinAttack.timersPaused = false;
        timersPaused = false;
    }

    public IEnumerator WaitForSelection()
    {
        selectText.SetActive(true);
        foreach (Combatant combatant in combatManager.combatants)
        {
            combatant.PauseTimer();
        }
        timeRemaining = maxCountDownTime;
        timersPaused = true;
        spinAttack.timersPaused = true;
        spinAttack.isActive = false;
        // pause everything
        yield return new WaitUntil(() => selectionMade);
        StartCoroutine(Stab(selection));
    }
}


// step 1: click the stab attack button
// step 2: pause every cooldown both enemy and player both autp attack and abilities
// step 3: allow player to make a selection of an enemy to target with the attack (combatManager.selection = true)
// step 4: once enemy is clicked, it takes the clicked enemy and applies StartCoroutine(Stab()) on it (stab will have a new argument when calling it