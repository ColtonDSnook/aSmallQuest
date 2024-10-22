using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpinAttack : Ability
{
    public int numTargets;
    public int baseDamage = 10;

    public PlayableDirector spinAttackTimeline;

    public override void UseAbility()
    {
        if (timeRemaining == 0)
        {
            if (combatManager.combatState == CombatManager.CombatState.InCombat)
            {
                //spinAttackTimeline.Play();
                ability.sprite = abilityGrey;
                abilityRadial.fillAmount = 1;
            }
        }
        else
        {
            Debug.Log("Wait for cooldown to finish");
            return;
        }

        // use spin attack timeline;

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
            int randomIndex = Random.Range(0, availableTargets.Count);

            Combatant selectedCombatant = availableTargets[randomIndex];

            if (availableTargets.Count == 1)
            {
                selectedTargets.Add(selectedCombatant);
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

        foreach (Combatant target in selectedTargets)
        {
            target.healthSystem.TakeDamage(baseDamage);
            Debug.Log("Spin attack hit " + target.name + " for " + baseDamage + " damage.");
        }

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
    }
}
