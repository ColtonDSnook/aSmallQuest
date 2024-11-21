using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GlobalVariables;

public class Combatant : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public ParticleSystem coin;

    public bool timersPaused = false;

    public GameObject test;

    public GameObject healthBarObject;
    public GameObject coolDownBarObject;

    public StabAttack stabAttack;

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
        stabAttack = FindObjectOfType<StabAttack>();
        test.SetActive(false);
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
        }
        else
        {
            // ui popup
            test.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (combatManager.selection)
        {
            // remove outline around selected combatant
        }
        else
        {
            // ui popup closed
            test.SetActive(false);
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
}
