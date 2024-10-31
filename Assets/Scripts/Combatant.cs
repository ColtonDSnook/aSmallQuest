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

    public string animPrefix;

    public enum CombatantType
    {
        Player,
        Slime,
        Goblin,
        Kobold,
        DungeonMaster
    }

    public CombatantType combatantType;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        combatManager = FindObjectOfType<CombatManager>();
        cooldownTimer = maxCooldownTimer;
        healthSystem = GetComponent<Health>();

        switch (combatantType)
        {
            case CombatantType.Slime:
                maxCooldownTimer = defaultCooldownTimer / defaultSlimeAttackSpeed;
                damage = defaultSlimeDamage;
                animPrefix = slimeAnimPrefix;
                break;
            case CombatantType.Goblin:
                maxCooldownTimer = defaultCooldownTimer / defaultGoblinAttackSpeed;
                damage = defaultGoblinDamage;
                animPrefix = goblinAnimPrefix;
                break;
            case CombatantType.Kobold:
                maxCooldownTimer = defaultCooldownTimer / defaultKoboldAttackSpeed;
                damage = defaultKoboldDamage;
                animPrefix = koboldAnimPrefix;
                break;
            case CombatantType.DungeonMaster:
                maxCooldownTimer = defaultCooldownTimer / defaultDungeonMasterAttackSpeed;
                damage = defaultDungeonMasterDamage;
                animPrefix = dungeonMasterAnimPrefix;
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

    public IEnumerator Kill()
    {
        yield return new WaitForSeconds(2);
        animator.Play(animPrefix + "_Death");
        Destroy(gameObject);
    }

    public void ResetCooldowns()
    {
        cooldownTimer = maxCooldownTimer;
    }

}
