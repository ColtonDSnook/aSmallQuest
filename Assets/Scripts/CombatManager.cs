using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private float cooldownTimer;
    public float maxCooldownTimer;

    [SerializeField] private bool abilityUsed;

    // when an ability is used, block the player from using any other ability until ability use is over.

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = maxCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            //attack
            cooldownTimer = maxCooldownTimer;
        }
        if (abilityUsed)
        {
            //use ability
            cooldownTimer = maxCooldownTimer;
        }
        // combat will occur automatically when the player encounters an enemy "group".
        // this will be performed by a cooldown that will fill a bar adding the attacking entity to the attack queue.
        // this queue will be interrupted when the player uses an active ability.
    }
}
