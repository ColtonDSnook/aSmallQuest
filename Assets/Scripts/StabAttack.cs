using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StabAttack : Ability
{
    public float baseDamage;

    public void Update()
    {
        baseDamage = gameManager.stabDamage;

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
                animator.Play("MC_Stab");
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

        StartCoroutine(Stab());

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
    }

    public IEnumerator Stab()
    {
        player.PauseTimer();
        yield return new WaitForSeconds(0.5f);
        int targetNumber = Random.Range(0, combatManager.CountOtherCombatants() - 1);
        Combatant target = combatManager.combatants[targetNumber];
        float damage = target.healthSystem.TakeDamage(gameManager.damage * baseDamage);
        player.healthSystem.Heal(damage * gameManager.healing);
        player.UnpauseTimer();
    }
}
