using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour
{
    [SerializeField] public float cooldownTimer;
    public float maxCooldownTimer;
    public bool player;

    public Image cooldownBar;

    public List<Ability> abilities;

    public CombatManager combatManager;
    public Health healthSystem;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
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

        cooldownBar.fillAmount = (float)cooldownTimer / maxCooldownTimer;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void ResetCooldowns()
    {
        cooldownTimer = maxCooldownTimer;
    }

}
