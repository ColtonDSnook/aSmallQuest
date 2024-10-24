using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpinAttack : Ability
{
    public float numTargets;
    public int baseDamage = 2; //200%

    public PlayableDirector spinAttackTimeline;

    public void Update()
    {
        numTargets = gameManager.numTargets;

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

            // CHANGE THIS FOR HIGHER TARGET COUNTS
            if (availableTargets.Count == numTargets - 1)
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
            target.healthSystem.TakeDamage(gameManager.damage * baseDamage);
            Debug.Log("Spin attack hit " + target.name + " for " + gameManager.damage * baseDamage + " damage.");
        }

        timeRemaining = maxCountDownTime;
        player.cooldownTimer = player.maxCooldownTimer;
    }
}
