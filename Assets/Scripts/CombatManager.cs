using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static GlobalVariables;

public class CombatManager : MonoBehaviour
{
    public enum CombatState
    {
        Start,
        None,
        InCombat,
        Won,
        Lost
    }

    public CombatState combatState;

    public CombatState previousCombatState;

    public List<Combatant> combatants;

    public CurrencyDropper currencyDropper;

    public UpgradeManager upgradeManager;

    public GameManager gameManager;

    //public GameObject abilitiesUI;

    public GameObject spinAttackUI;
    public GameObject stabAttackUI;

    public Combatant player;

    public Health playerHealth;

    public LevelManager levelManager;

    public PlayerStats playerStats;

    public Ability spinAttack;
    public Ability stabAttack;


    //Variables for end of run stats
    public int coinsGainedCurrentRun;
    public int enemiesDefeatedCurrentRun;
    public float coinsTotal;

    public TextMeshProUGUI coinsGainedText;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI coinsTotalText;

    public TextMeshProUGUI coinsGainedWonText;
    public TextMeshProUGUI enemiesDefeatedWonText;
    public TextMeshProUGUI coinsTotalWonText;

    [SerializeField] private Combatant currentTarget;

    [SerializeField] private int encountersCompleted = 0;
    private int encountersRequired = defaultEncountersRequired;
    public TextMeshProUGUI encountersText;

    public Slider progressBar;

    public GameObject enemyStatsUI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI attackSpeedText;

    public bool lostCombat = false;
    public bool selection = false;

    public Combatant hoveredOver;

    // when an ability is used, block the player from using any other ability until ability use is over.

    // Start is called before the first frame update
    void Start()
    {
        coinsGainedCurrentRun = 0;
        enemiesDefeatedCurrentRun = 0;

        upgradeManager = FindObjectOfType<UpgradeManager>();
        //combatants.Add(player);
        combatState = CombatState.None;
        levelManager = FindObjectOfType<LevelManager>();
        currencyDropper = FindObjectOfType<CurrencyDropper>();
        //abilitiesUI.SetActive(false);
        lostCombat = false;
        encountersCompleted = 0;
        progressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.value = encountersCompleted;
        //encountersText.text = "Encounters Completed: " + encountersCompleted + "/" + encountersRequired;

        if (encountersCompleted >= encountersRequired)
        {
            DisplayEndResults(true);

            encountersCompleted = 0;
            progressBar.value = 0;
            levelManager.LoadScene("Post-Run", true);
            gameManager.Save();
            playerHealth.SetCurrentHealth();
            player.ResetCooldowns();
            lostCombat = false;
            player.UnpauseTimer();
            spinAttack.isActive = true;
            stabAttack.isActive = true;
            player.healthBarObject.SetActive(true);
            player.coolDownBarObject.SetActive(true);
        }

        //Debug.Log(player.healthSystem.GetCurrentHealth());
        if (combatState == CombatState.Start)
        {
            InitializeCombatants();
            //abilitiesUI.SetActive(true);
            CheckAbilities();
            player.UnpauseTimer();
            spinAttack.isActive = true;
            stabAttack.isActive = true;
            Debug.Log("Combat Started");
            combatState = CombatState.InCombat;
        }

        if (combatState == CombatState.InCombat)
        {
            InCombat();
        }

        if (combatState == CombatState.Lost)
        {
            DisplayEndResults(false);
            levelManager.LoadScene("Post-Run", false);

            ResetAll();
        }

        if (combatState == CombatState.Won)
        {
            combatants.Clear();
            previousCombatState = combatState;
            combatState = CombatState.None;
            //abilitiesUI.SetActive(false);
            player.UnpauseTimer();
            spinAttack.isActive = true;
            stabAttack.isActive = true;
            //playerHealth.SetCurrentHealth();
            //levelManager.LoadScene("Post-Run");
        }
        // when attacking the timer will pause for all entities and will let the entity attack.
        // combat will occur automatically when the player encounters an enemy "group".
        // this will be performed by a cooldown that will fill a bar adding the attacking entity to the attack queue.
        // this queue will be interrupted when the player uses an active ability.
    }

    public void InCombat()
    {
        foreach (Combatant combatant in combatants)
        {
            if (combatant.cooldownTimer <= 0)
            {
                if (!combatant.player)
                {
                    StartCoroutine(EnemyAttack(combatant, GetPlayer(), combatant.damage));
                }
                else
                {
                    int targetNumber = Random.Range(0, CountOtherCombatants() - 1);
                    StartCoroutine(Attack(combatants[targetNumber]));
                }
                //this attack will be in the form of a timeline that will send a signal to deal damage to the opposing force.
                combatant.cooldownTimer = combatant.maxCooldownTimer;
            }

            if (combatant.healthSystem.GetCurrentHealth() <= 0)
            {
                //Debug.Log(combatant.healthSystem.GetCurrentHealth());
                if (combatant.player && !lostCombat)
                {
                    StartCoroutine(LoseCombat());
                    //upgradeManager.Save();
                }
                else if (!combatant.player)
                {
                    combatants.Remove(combatant);
                    coinsGainedCurrentRun += currencyDropper.DropCurrency();
                    enemiesDefeatedCurrentRun++;
                    StartCoroutine(combatant.Kill());
                }
            }
        }
        if (CountOtherCombatants() == 0)
        {
            combatState = CombatState.Won;
            encountersCompleted++;
            //Debug.Log(playerStats.GetStat("Health"));

        }
    }

    public IEnumerator LoseCombat()
    {
        spinAttack.isActive = false;
        stabAttack.isActive = false;
        foreach (Combatant combatant in combatants)
        {
            combatant.PauseTimer();
        }
        player.healthBarObject.SetActive(false);
        player.coolDownBarObject.SetActive(false);
        player.animator.Play("MC_Death");
        lostCombat = true;
        yield return new WaitForSeconds(3);
        combatState = CombatState.Lost;
    }

    public IEnumerator Attack(Combatant target)
    {
        player.PauseTimer();
        player.animator.Play("MC_Slash");
        yield return new WaitForSeconds(0.5f);
        target.healthSystem.TakeDamage(gameManager.damage);
        currentTarget = target;
        player.UnpauseTimer();
        Debug.Log("Attacked");
    }

    public IEnumerator EnemyAttack(Combatant user, Combatant target, float damage)
    {
        user.animator.Play(user.animPrefix + "_Attack");
        yield return new WaitForSeconds(user.attackAnimTime);
        if (user.healthSystem.GetCurrentHealth() >= 0)
        {
            target.healthSystem.TakeDamage(damage);
        }
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

    public void InitializeCombatants()
    {
        Combatant[] combatantsArray = FindObjectsOfType<Combatant>();
        combatants = combatantsArray.ToList<Combatant>();
    }

    public void ResetCombatState()
    {
        combatState = CombatState.None;
    }

    public void ResetAll()
    {
        ResetCombatState();
        playerHealth.SetCurrentHealth();
        foreach (Combatant combatant in combatants)
        {
            combatant.ResetCooldowns();
            combatant.healthSystem.damageText.gameObject.SetActive(false);
        }
        player.ResetCooldowns();
        stabAttack.RefreshAbility();
        spinAttack.RefreshAbility();
        coinsGainedCurrentRun = 0;
        enemiesDefeatedCurrentRun = 0;
        encountersCompleted = 0;
        progressBar.value = 0;
        lostCombat = false;
        player.UnpauseTimer();
        player.healthBarObject.SetActive(true);
        player.coolDownBarObject.SetActive(true);
        spinAttack.isActive = true;
        stabAttack.isActive = true;
    }

    public void DisplayEndResults(bool won)
    {
        if (!won)
        {
            coinsGainedText.text = "Coins Collected: " + coinsGainedCurrentRun;
            enemiesDefeatedText.text = "Enemies Defeated: " + enemiesDefeatedCurrentRun;
            coinsTotal = gameManager.gold;
            coinsTotalText.text = "Coins Total: " + "\n" + coinsTotal;
        }
        else if (won)
        {
            coinsGainedWonText.text = "Coins Collected: " + coinsGainedCurrentRun;
            enemiesDefeatedWonText.text = "Enemies Defeated: " + enemiesDefeatedCurrentRun;
            coinsTotal = gameManager.gold;
            coinsTotalWonText.text = "Coins Total: " + "\n" + coinsTotal;
        }
    }

    public void CheckAbilities()
    {
        if (gameManager.spinAttack)
        {
            spinAttackUI.SetActive(true);
        }
        else
        {
            spinAttackUI.SetActive(false);
        }

        if (gameManager.stabAttack)
        {
            stabAttackUI.SetActive(true);
        }
        else
        {
            stabAttackUI.SetActive(false);
        }
    }
}
