using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GlobalVariables;

public class Combatant : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public float cooldownTimer;
    public float maxCooldownTimer;
    public float damage;
    public float attackSpeed;

    public float defaultCooldownTimer = defaultPlayerCooldown;
    public bool player;

    public Image cooldownBar;

    public List<Ability> abilities;

    public CombatManager combatManager;
    public Health healthSystem;

    public Animator animator;

    public string animPrefix;

    public float attackAnimTime;

    public ParticleSystem coin;

    public bool timersPaused = false;


    public GameObject healthBarObject;
    public GameObject coolDownBarObject;

    public StabAttack stabAttack;

    public SelectionShader selectionShader;

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
    public void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        stabAttack = FindObjectOfType<StabAttack>();
        combatManager.enemyStatsUI.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<Health>();
        selectionShader = GetComponentInChildren<SelectionShader>();

        switch (combatantType)
        {
            case CombatantType.Slime:
                InitEnemyStats(defaultSlimeAttackSpeed, defaultSlimeDamage, slimeAnimPrefix, defaultSlimeAttackAnimTime);
                break;
            case CombatantType.Goblin:
                InitEnemyStats(defaultGoblinAttackSpeed, defaultGoblinAttackSpeed, goblinAnimPrefix, defaultGoblinAttackAnimTime);
                break;
            case CombatantType.Kobold:
                InitEnemyStats(defaultKoboldAttackSpeed, defaultKoboldAttackSpeed, koboldAnimPrefix, defaultKoboldAttackAnimTime);
                break;
            case CombatantType.DungeonMaster:
                InitEnemyStats(defaultDungeonMasterAttackSpeed, defaultDungeonMasterDamage, dungeonMasterAnimPrefix, defaultDungeonMasterAttackAnimTime);
                break;
        }
        cooldownTimer = maxCooldownTimer;
    }

    // Update is called once per frame
    public void Update()
    {
        if (player)
        {
            maxCooldownTimer = defaultCooldownTimer / GameManager.manager.attackSpeed;
            damage = GameManager.manager.damage;
            attackSpeed = GameManager.manager.attackSpeed;
        }

        if (combatManager.combatState == CombatManager.CombatState.InCombat)
        {
            if (!timersPaused)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        cooldownBar.fillAmount = (float)cooldownTimer / maxCooldownTimer;

        if (combatManager.hoveredOver != null)
        {
            if (this == combatManager.hoveredOver)
            {
                HandleHover();
            }
        }
    }

    public IEnumerator Kill()
    {
        animator.Play(animPrefix + "_Death");
        PauseTimer();
        coolDownBarObject.SetActive(false);
        healthBarObject.SetActive(false);
        coin.Play();
        SoundManager.Instance.PlaySFX("Coin");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void ResetCooldowns()
    {
        cooldownTimer = maxCooldownTimer;
    }

    public void PauseTimer()
    {
        Debug.Log("Paused Timer");
        timersPaused = true;
    }

    public void UnpauseTimer()
    {
        if (!combatManager.lostCombat)
        {
            Debug.Log("Unpaused Timer");
            timersPaused = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // if attack selection is active, do one thing,
        // if not, show stats popup for combatant
        if (combatManager.selection)
        {
            // selction ui up like an outline or something around them and if clicked
            // this.Combatant should be passed into a function where it is the target of the attack
            selectionShader.SelectionOn();
        }
        else
        {
            // ui popup
            combatManager.enemyStatsUI.SetActive(true);
            selectionShader.SelectionOn();
            combatManager.hoveredOver = this;

            HandleHover();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (combatManager.selection)
        {
            // remove outline around selected combatant
            selectionShader.SelectionOff();
        }
        else
        {
            // ui popup closed
            combatManager.enemyStatsUI.SetActive(false);
            selectionShader.SelectionOff();
        }
    }

    public void OnMouseDown()
    {
        if (combatManager.selection)
        {
            stabAttack.selection = this;
            stabAttack.selectionMade = true;
        }
        Debug.Log("Clicked On:" + this.name);
    }

    public void InitEnemyStats(float attackSpeed, float damage, string animPrefix, float attackAnimTime)
    {
        maxCooldownTimer = defaultCooldownTimer / attackSpeed;
        this.attackSpeed = attackSpeed;
        this.damage = damage;
        this.animPrefix = animPrefix;
        this.attackAnimTime = attackAnimTime;
    }

    public void HandleHover()
    {
        if (this.name == "Player")
        {
            combatManager.nameText.text = "Name: " + name;
        }
        else
        {
            combatManager.nameText.text = "Name: " + TrimString(name);
        }
        combatManager.healthText.text = "Health: " + healthSystem.GetCurrentHealth() + "/" + healthSystem.maxHealth;
        combatManager.damageText.text = "Damage: " + damage;
        combatManager.attackSpeedText.text = "Atk Spd: " + attackSpeed;
    }

    public string TrimString(string str)
    {
        char[] stringArray = str.ToCharArray();
        Array.Reverse(stringArray);
        string reversedStr = new string(stringArray);
        string trimmedReverseStr = reversedStr.Remove(0, 7);
        char[] trimmedStrArr = trimmedReverseStr.ToCharArray();
        Array.Reverse(trimmedStrArr);
        string rightString = new string(trimmedStrArr);

        return rightString;
    }
}
