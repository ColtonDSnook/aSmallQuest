using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
    public SaveManager saveManager;
    public UIManager uiManager;

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
        //IMPORTANT
        //increment progress bar by one at the beginning of each encounter
        //increment the amount of encounters completed at the end of each encounter
        CheckAbilities();
        if (encountersCompleted >= encountersRequired)
        {
            WinGame();
        }
        HandleCombatState();
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
            if (player.attackInst != null)
            {
                StopCoroutine(player.attackInst);
            }
            StartCoroutine(LoseCombat());
        }
        else if (!combatant.player)
        {
            combatants.Remove(combatant);
            coinsGainedCurrentRun += currencyDropper.DropCurrency();
            enemiesDefeatedCurrentRun++;
            if (combatant.attackInst != null)
            {
                StopCoroutine(combatant.attackInst);
            }
            StartCoroutine(combatant.Kill());
            uiManager.AnimateCoinCounter();
        }
    }

    public void StartCombat()
    {
        InitializeCombatants();
        StaggerCooldowns();
        player.UnpauseTimer();
        EnableAbilities();
        progressBar.value++;
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
                    combatant.attackInst = StartCoroutine(combatant.Attack(GetPlayer(), combatant.damage));
                }
                else
                {
                    HandleCombatTargeting();
                }
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
        saveManager.Save();
        ResetAll();
    }

    public void WinGame()
    {
        DisplayEndResults(true);

        encountersCompleted = 0;
        progressBar.value = 0;
        levelManager.LoadScene("Post-Run", true);
        saveManager.Save();
        playerHealth.SetCurrentHealth();
        player.ResetCooldowns();
        lostCombat = false;
        player.UnpauseTimer();
        spinAttack.isActive = true;
        stabAttack.isActive = true;
        player.healthBarObject.SetActive(true);
        player.coolDownBarObject.SetActive(true);
    }

    public IEnumerator LoseCombat()
    {
        DisableAbilities();
        foreach (Combatant combatant in combatants)
        {
            combatant.PauseTimer();
            if (combatant.attackInst != null)
            {
                StopCoroutine(combatant.attackInst);
            }
        }
        player.healthBarObject.SetActive(false);
        player.coolDownBarObject.SetActive(false);
        player.animator.Play("MC_Death");
        lostCombat = true;
        yield return new WaitForSeconds(3);
        combatState = CombatState.Lost;
    }

    public void HandleCombatTargeting()
    {
        if (combatants.Any(c => c.isTargeted))
        {
            foreach (Combatant otherCombatant in combatants)
            {
                if (otherCombatant.isTargeted)
                {
                    player.attackInst = StartCoroutine(player.Attack(otherCombatant, gameManager.damage));
                }
            }
        }
        else
        {
            int targetNumber = Random.Range(0, CountOtherCombatants());
            Debug.Log(targetNumber);
            player.attackInst = StartCoroutine(player.Attack(combatants[targetNumber], gameManager.damage));
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
        saveManager = FindObjectOfType<SaveManager>();
        uiManager = FindObjectOfType<UIManager>();
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
        Time.timeScale = 1;
        gameManager.isSpedUp = false;
        ResetCombatState();
        playerHealth.SetCurrentHealth();
        foreach (Combatant combatant in combatants)
        {
            combatant.ResetCooldowns();
            combatant.healthSystem.damageText.gameObject.SetActive(false); // !
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
        player.healthBarObject.SetActive(true); // !
        player.coolDownBarObject.SetActive(true); // !
        EnableAbilities();
    }

    public void DisplayEndResults(bool won)
    {
        uiManager.DisplayEndResults(won, coinsGainedCurrentRun, enemiesDefeatedCurrentRun, gameManager.gold);
    }

    public void CheckAbilities()
    {
        uiManager.CheckAbilities(gameManager.spinAttack, gameManager.stabAttack);
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
}
