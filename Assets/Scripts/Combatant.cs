using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    [SerializeField] public float cooldownTimer;
    public float maxCooldownTimer;
    public bool player;

    public List<Ability> abilities;

    public CombatManager combatManager;
    public Health healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        cooldownTimer = maxCooldownTimer;
        healthSystem = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.combatState == CombatManager.CombatState.InCombat)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
