using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class Combatant : MonoBehaviour
{
    [SerializeField] public float cooldownTimer;
    public float maxCooldownTimer;
    public float damage;

    public float defaultCooldownTimer = defaultPlayerCooldown;
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

        switch (name)
        {
            case "Slime(Clone)":
                maxCooldownTimer = defaultCooldownTimer / defaultSlimeAttackSpeed;
                damage = defaultSlimeDamage;
                break;
            case "Goblin(Clone)":
                maxCooldownTimer = defaultCooldownTimer / defaultGoblinAttackSpeed;
                damage = defaultGoblinDamage;
                break;
            case "Kobold(Clone)":
                maxCooldownTimer = defaultCooldownTimer / defaultKoboldAttackSpeed;
                damage = defaultKoboldDamage;
                break;
            case "DungeonMaster(Clone)":
                maxCooldownTimer = defaultCooldownTimer / defaultDungeonMasterAttackSpeed;
                damage = defaultDungeonMasterDamage;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            maxCooldownTimer = defaultCooldownTimer / GameManager.manager.attackSpeed;
            damage = GameManager.manager.damage;
        }

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
