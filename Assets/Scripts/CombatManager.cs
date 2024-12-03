using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Timeline;
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
    public LevelManager levelManager;
    public GameManager gameManager;

    //public GameObject abilitiesUI;

    public GameObject spinAttackUI;
    public GameObject stabAttackUI;

    public Combatant player;
    public Health playerHealth;
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

    public RectTransform coinCounterUI;
    public float coinCounterScale = 0.42f;
    public float newCoinScale = 0.5f;
    public float tweenSpeed = 0.5f;

    // when an ability is used, block the player from using any other ability until ability use is over.

    // Start is called before the first frame update
    public void Start()
    {
        InitializeManagers();
        ResetRunStats();

        combatState = CombatState.None;
        lostCombat = false;
        progressBar.value = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        progressBar.value = encountersCompleted;
        CheckAbilities();
        WinGame();
        HandleCombatState();
        // when attacking the timer will pause for all entities and will let the entity attack.
        // combat will occur automatically when the player encounters an enemy "group".
        // this will be performed by a cooldown that will fill a bar adding the attacking entity to the attack queue.
        // this queue will be interrupted when the player uses an active ability.
    }

    public void HandleCombatState()
    {
        switch (combatState)
        {
            case CombatState.Start:
                StartCombat();
                break;
            case CombatState.InCombat:
                InCombat();
                break;
            case CombatState.Lost:
                LostCombat();
                break;
            case CombatState.Won:
                WonCombat();
                break;
        }
    }

    public void HandleCombatantDeath(Combatant combatant)
    {
        if (combatant.player && !lostCombat)
        {
            StartCoroutine(LoseCombat());
        }
        else if (!combatant.player)
        {
            combatants.Remove(combatant);
            coinsGainedCurrentRun += currencyDropper.DropCurrency();
            enemiesDefeatedCurrentRun++;
            StartCoroutine(combatant.Kill());
            AnimateCoinCounter();
        }
    }

    public void StartCombat()
    {
        InitializeCombatants();
        StaggerCooldowns();
        player.UnpauseTimer();
        EnableAbilities();
        combatState = CombatState.InCombat;
    }

    private void StaggerCooldowns()
    {
        float staggerInterval = 2f;
        float currentStagger = 0f;

        foreach (Combatant combatant in combatants)
        {
            if (!combatant.player)
            {
                combatant.cooldownTimer += currentStagger;
                currentStagger += staggerInterval;
            }
        }
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
                HandleCombatantDeath(combatant);
            }
        }
        if (CountOtherCombatants() == 0)
        {
            combatState = CombatState.Won;
            encountersCompleted++;
        }
    }

    public void WonCombat()
    {
        combatants.Clear();
        previousCombatState = combatState;
        combatState = CombatState.None;
        player.UnpauseTimer();
        EnableAbilities();
    }

    public void LostCombat()
    {
        DisplayEndResults(false);
        levelManager.LoadScene("Post-Run", false);
        gameManager.Save();
        ResetAll();
    }

    public void WinGame()
    {
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
    }

    public IEnumerator LoseCombat()
    {
        DisableAbilities();
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
        stabAttack.isActive = false;
        player.animator.Play("MC_Slash");
        yield return new WaitForSeconds(0.5f);
        target.healthSystem.TakeDamage(gameManager.damage);
        player.UnpauseTimer();
        stabAttack.isActive = true;
    }

    public IEnumerator EnemyAttack(Combatant user, Combatant target, float damage)
    {
        user.animator.Play(user.animPrefix + "_Attack");
        yield return new WaitForSeconds(user.attackAnimTime);
        if (user.healthSystem.GetCurrentHealth() >= 0)
        {
            target.healthSystem.TakeDamage(damage);
        }
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

    public void InitializeManagers()
    {
        levelManager = FindObjectOfType<LevelManager>();
        currencyDropper = FindObjectOfType<CurrencyDropper>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    public void InitializeCombatants()
    {
        combatants = FindObjectsOfType<Combatant>().ToList();
    }

    public void ResetRunStats()
    {
        coinsGainedCurrentRun = 0;
        enemiesDefeatedCurrentRun = 0;
        encountersCompleted = 0;
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
        EnableAbilities();
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
        spinAttackUI.SetActive(gameManager.spinAttack);
        stabAttackUI.SetActive(gameManager.stabAttack);
    }

    public void EnableAbilities()
    {
        spinAttack.isActive = true;
        stabAttack.isActive = true;
    }

    public void DisableAbilities()
    {
        spinAttack.isActive = false;
        stabAttack.isActive = false;
    }

    public void AnimateCoinCounter()
    {
        coinCounterUI.DOScale(new Vector3(newCoinScale, newCoinScale, 0f), tweenSpeed).From(coinCounterScale);
        coinCounterUI.DOScale(new Vector3(coinCounterScale, coinCounterScale, 0f), tweenSpeed).From(newCoinScale);
    }
}
