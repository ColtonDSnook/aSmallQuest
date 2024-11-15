using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using static GlobalVariables;

public class SpinAttack : Ability
{
    public StabAttack stabAttack;
    public float numTargets;
    public float bursts = defaultBursts;
    private float baseDamage = spinAttackBaseDamage; //40%

    public void Update()
    {
        numTargets = gameManager.numTargets;
        bursts = gameManager.bursts;

        if (timeRemaining > 0)
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
                abilityRadial.fillAmount = 1;
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

        List<Combatant> availableTargets = new List<Combatant>();

        foreach (Combatant combatant in combatManager.combatants)
        {
            if (!combatant.player)
            {
                availableTargets.Add(combatant);
            }
        }

        if (availableTargets.Count == 0)
        {
            return;
        }

        List<Combatant> selectedTargets = new List<Combatant>();
        while (selectedTargets.Count < numTargets)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableTargets.Count);

            Combatant selectedCombatant = availableTargets[randomIndex];

            if (availableTargets.Count < numTargets)
            {
                foreach (Combatant combatant in availableTargets)
                {
                    selectedTargets.Add(combatant);
                }
                break;
            }
            else
            {
                if (!selectedTargets.Contains(selectedCombatant))
                {
                    selectedTargets.Add(selectedCombatant);
                }
            }
        }

        StartCoroutine(SpinAttackBursts(selectedTargets));

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
        Debug.Log("Ability Completed");
    }

    public IEnumerator SpinAttackBursts(List<Combatant> selectedTargets)
    {
        player.PauseTimer();
        stabAttack.isActive = false;
        for (int i = 0; i < bursts; i++)
        {
            animator.Play("MC_SpinSlash");
            yield return new WaitForSeconds(0.5f);
            foreach (Combatant target in selectedTargets)
            {
                // Check if the target is still alive
                if (target.healthSystem.GetCurrentHealth() > 0)
                {
                    float damage = target.healthSystem.TakeDamage((float)Math.Round(gameManager.damage * baseDamage));
                    Debug.Log("Spin attack hit " + target.name + " for " + damage + " damage.");
                }
            }

            // Pause briefly between bursts (if needed)
            yield return new WaitForSeconds(0.35f);
        }
        player.UnpauseTimer();
        stabAttack.isActive = true;
    }
}
