using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    [SerializeField] public float cooldownTimer;
    public float maxCooldownTimer;
    public bool player;

    public Health healthSystem;

    [SerializeField] public bool abilityUsed;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = maxCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (abilityUsed)
        {
            //play timeline for ability
            cooldownTimer = maxCooldownTimer;
        }
    }
}
