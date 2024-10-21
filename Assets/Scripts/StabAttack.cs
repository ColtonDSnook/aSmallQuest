using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StabAttack : Ability
{
    public int baseDamage;
    public PlayableDirector stabAttackTimeline;

    public override void UseAbility()
    {
        if (timeRemaining == 0)
        {
            if (combatManager.combatState == CombatManager.CombatState.InCombat)
            {
                //stabAttackTimeline.Play();
                ability.sprite = abilityGrey;
                abilityRadial.fillAmount = 1;
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

        int targetNumber = Random.Range(0, combatManager.CountOtherCombatants() - 1);
        Combatant target = combatManager.combatants[targetNumber];
        target.healthSystem.TakeDamage(baseDamage);

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
    }
}
