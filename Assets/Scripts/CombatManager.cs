using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

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

    public GameObject abilitiesUI;

    public Combatant player;

    public Health playerHealth;

    public PlayableDirector slashTimeline;

    public LevelManager levelManager;

    public PlayerStats playerStats;


    //Variables for end of run stats
    public int coinsGainedCurrentRun;
    public int enemiesDefeatedCurrentRun;
    public int coinsTotal;

    public TextMeshProUGUI coinsGainedText;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI coinsTotalText;

    public TextMeshProUGUI coinsGainedWonText;
    public TextMeshProUGUI enemiesDefeatedWonText;
    public TextMeshProUGUI coinsTotalWonText;

    [SerializeField] private Combatant currentTarget;

    [SerializeField] private int encountersCompleted = 0;
    public int encountersRequired = 5;
    public TextMeshProUGUI encountersText;

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
        abilitiesUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        encountersText.text = "Encounters Completed: " + encountersCompleted + "/" + encountersRequired;

        if (encountersCompleted >= encountersRequired)
        {
            coinsGainedWonText.text = "Coins Collected: " + coinsGainedCurrentRun;
            enemiesDefeatedWonText.text = "Enemies Defeated: " + enemiesDefeatedCurrentRun;
            coinsTotal = gameManager.gold;
            coinsTotalWonText.text = "Coins Total: " + "\n" + coinsTotal;

            encountersCompleted = 0;
            levelManager.LoadScene("Post-Run", true);
            gameManager.Save();
            playerHealth.SetCurrentHealth();
            player.ResetCooldowns();
        }

        //Debug.Log(player.healthSystem.GetCurrentHealth());
        if (combatState == CombatState.Start)
        {
            InitializeCombatants();
            abilitiesUI.SetActive(true);
            Debug.Log("Combat Started");
            combatState = CombatState.InCombat;
        }

        if (combatState == CombatState.InCombat)
        {
            InCombat();
        }

        if (combatState == CombatState.Lost)
        {
            coinsGainedText.text = "Coins Collected: " + coinsGainedCurrentRun;
            enemiesDefeatedText.text = "Enemies Defeated: " + enemiesDefeatedCurrentRun;
            coinsTotal = gameManager.gold;
            coinsTotalText.text = "Coins Total: " + "\n" + coinsTotal;

            encountersCompleted = 0;
            levelManager.LoadScene("Post-Run", false);
            combatants.Clear();
            gameManager.Save();
            previousCombatState = combatState;
            combatState = CombatState.None;
            playerHealth.SetCurrentHealth();
            player.ResetCooldowns();
        }

        if (combatState == CombatState.Won)
        {
            combatants.Clear();
            previousCombatState = combatState;
            combatState = CombatState.None;
            abilitiesUI.SetActive(false);
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
                    EnemyAttack(GetPlayer());
                    combatant.animator.Play("Slime_Attack");
                }
                else
                {
                    int targetNumber = Random.Range(0, CountOtherCombatants() - 1);
                    Attack(combatants[targetNumber]);
                }
                //this attack will be in the form of a timeline that will send a signal to deal damage to the opposing force.
                combatant.cooldownTimer = combatant.maxCooldownTimer;
            }

            if (combatant.healthSystem.GetCurrentHealth() <= 0)
            {
                //Debug.Log(combatant.healthSystem.GetCurrentHealth());
                if (combatant.player)
                {
                    playerHealth.SetCurrentHealth();
                    combatant.ResetCooldowns();
                    combatState = CombatState.Lost;
                    //upgradeManager.Save();
                }
                else
                {
                    combatants.Remove(combatant);
                    coinsGainedCurrentRun += currencyDropper.DropCurrency();
                    enemiesDefeatedCurrentRun++;
                    combatant.Kill();
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

    public void Attack(Combatant target)
    {
        //slashTimeline.Play();
        target.healthSystem.TakeDamage(4);
        player.animator.Play("MC_Slash");
        currentTarget = target;

        Debug.Log("Attacked");
    }

    public void EnemyAttack(Combatant target)
    {
        //slashTimeline.Play();
        target.healthSystem.TakeDamage(4);

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

    public void ApplyDamage()
    {
        currentTarget.healthSystem.TakeDamage(4);
    }

    public void ResetCombatState()
    {
        combatState = CombatState.None;
    }
}
