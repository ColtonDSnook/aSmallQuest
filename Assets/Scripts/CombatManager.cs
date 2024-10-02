using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public enum CombatState
    {
        None,
        InCombat,
        Won,
        Lost
    }

    public CombatState combatState;

    public List<Combatant> combatants;

    public Combatant player;


    public LevelManager levelManager;

    // when an ability is used, block the player from using any other ability until ability use is over.

    // Start is called before the first frame update
    void Start()
    {
        combatants.Add(player);
        combatState = CombatState.None;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        while (combatState == CombatState.InCombat)
        {
            foreach (Combatant combatant in combatants)
            {
                if (combatant.cooldownTimer <= 0)
                {
                    if (!combatant.player)
                    {
                        Attack(GetPlayer());
                    }
                    else
                    {
                        int targetNumber = Random.Range(1, CountOtherCombatants());
                        Attack(combatants[targetNumber]);
                    }
                    //this attack will be in the form of a timeline that will send a signal to deal damage to the opposing force.
                    combatant.cooldownTimer = combatant.maxCooldownTimer;
                }
                if (combatant.abilityUsed)
                {
                    //use ability
                    combatant.cooldownTimer = combatant.maxCooldownTimer;
                    combatant.abilityUsed = false;
                }

                if (combatant.healthSystem.GetCurrentHealth() <= 0)
                {
                    if (combatant.player)
                    {
                        combatState = CombatState.Lost;
                    }
                    else
                    {
                        combatants.Remove(combatant);
                        Destroy(combatant);
                        //kill
                    }
                }
            }
        }

        if (combatState == CombatState.Lost)
        {
            combatants.Clear();
            levelManager.LoadScene("Post-Run");
        }
        // when attacking the timer will pause for all entities and will let the entity attack.
        // combat will occur automatically when the player encounters an enemy "group".
        // this will be performed by a cooldown that will fill a bar adding the attacking entity to the attack queue.
        // this queue will be interrupted when the player uses an active ability.
    }

    public void Attack(Combatant target)
    {
        target.healthSystem.TakeDamage(4);
        Debug.Log("Attacked");
    }

    public Combatant GetPlayer()
    {
        foreach (Combatant combatant in combatants)
        {
            if (combatant.player)
            {
                return combatant;
            }
        }
        return null;
    }

    public int CountOtherCombatants()
    {
        int count = 0;
        foreach (Combatant combatant in combatants)
        {
            if (!combatant.player)
            {
                count++;
            }
        }
        return count;
    }
}
