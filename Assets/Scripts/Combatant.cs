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

    public float attackAnimTime;

    public GameObject gold;

    public bool timersPaused = false;

    public GameObject healthBarObject;
    public GameObject coolDownBarObject;

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
        if (!player)
        {
            gold.SetActive(false);
        }
        animator = GetComponentInChildren<Animator>();
        combatManager = FindObjectOfType<CombatManager>();
        healthSystem = GetComponent<Health>();

        switch (combatantType)
        {
            case CombatantType.Slime:
                maxCooldownTimer = defaultCooldownTimer / defaultSlimeAttackSpeed;
                damage = defaultSlimeDamage;
                animPrefix = slimeAnimPrefix;
                attackAnimTime = defaultSlimeAttackAnimTime;
                break;
            case CombatantType.Goblin:
                maxCooldownTimer = defaultCooldownTimer / defaultGoblinAttackSpeed;
                damage = defaultGoblinDamage;
                animPrefix = goblinAnimPrefix;
                attackAnimTime = defaultGoblinAttackAnimTime;
                break;
            case CombatantType.Kobold:
                maxCooldownTimer = defaultCooldownTimer / defaultKoboldAttackSpeed;
                damage = defaultKoboldDamage;
                animPrefix = koboldAnimPrefix;
                attackAnimTime = defaultKoboldAttackAnimTime;
                break;
            case CombatantType.DungeonMaster:
                maxCooldownTimer = defaultCooldownTimer / defaultDungeonMasterAttackSpeed;
                damage = defaultDungeonMasterDamage;
                animPrefix = dungeonMasterAnimPrefix;
                attackAnimTime = defaultDungeonMasterAttackAnimTime;
                break;
        }
        cooldownTimer = maxCooldownTimer;
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
            if (!timersPaused)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        cooldownBar.fillAmount = (float)cooldownTimer / maxCooldownTimer;
    }

    public IEnumerator Kill()
    {
        animator.Play(animPrefix + "_Death");
        PauseTimer();
        coolDownBarObject.SetActive(false);
        healthBarObject.SetActive(false);
        gold.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void ResetCooldowns()
    {
        cooldownTimer = maxCooldownTimer;
    }

    public void PauseTimer()
    {
        timersPaused = true;
    }

    public void UnpauseTimer()
    {
        if (!combatManager.lostCombat)
        {
            timersPaused = false;
        }
    }

}
