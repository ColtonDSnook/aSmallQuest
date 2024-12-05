using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public UpgradeManager upgradeManager;
    public CombatManager combatManager;
    public UIManager uiManager;
    public SaveManager saveManager;

    private PlayerMovement player;
    public Combatant playerCombatant;
    public GameObject playerSprite;
    public GameObject spawnPoint;

    public SpinAttack spin;
    public StabAttack stab;

    public Button playButton;

    public TextMeshProUGUI versionNumber;

    public Image speedupImage;
    public Sprite speedupSprite;
    public Sprite normalSpeedSprite;

    public bool isSpedUp = false;

    public float gold = defaultGold; //#
    public float damage = defaultDamage; //#
    public float maxHealth = defaultMaxHealth; //#
    public float attackSpeed = defaultAttackSpeed; //%

    public bool spinAttack = defaultSpinAttack;
    //stats for spin attack
    public float numTargets = defaultNumTargets; //#
    public float bursts = defaultBursts; //#

    public bool stabAttack = defaultStabAttack;
    //stats for stab attack
    public float healing = defaultHealing; //%
    public float stabDamage = defaultStabDamage; // 1000% base

    public enum GameState
    {
        MainMenu,
        Gameplay,
        RunEnd,
        Pause,
        Credits,
        Upgrades,
        RunWin,
        Settings,
        Intro,
        Controls
    }

    public GameState previousGameState;
    public GameState gameState;

    private string saveFilePath;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        HandleGameState();
        HandleSpeedupButtonChanges();
    }

    void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Gameplay:
                Gameplay();
                break;
            case GameState.Pause:
                Pause();
                break;
            case GameState.Settings:
                Settings();
                break;
            case GameState.RunEnd:
                RunEnd();
                break;
            case GameState.Credits:
                Credits();
                break;
            case GameState.Upgrades:
                Upgrades();
                break;
            case GameState.RunWin:
                RunWin();
                break;
            case GameState.Intro:
                Intro();
                break;
            case GameState.Controls:
                Controls();
                break;
        }
    }

    void InitializeGame()
    {
        uiManager.choicePrompt.SetActive(false);
        playButton.Select();
        versionNumber.text = Application.version;
        combatManager = FindObjectOfType<CombatManager>();
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
        saveManager = FindObjectOfType<SaveManager>();
        gameState = GameState.MainMenu;
        stab.selectText.SetActive(false);

        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";
    }

    #region GameStates
    private void MainMenu()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIMainMenu();
    }

    private void Gameplay()
    {
        Cursor.visible = true;
        playerSprite.SetActive(true);
        //lastPlayerPosition = player.transform.position;
        uiManager.UIGameplay();
    }

    private void Settings()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UISettings();
    }

    private void Pause()
    {
        Cursor.visible = true;
        playerSprite.SetActive(true);
        uiManager.UIPause();
    }

    private void RunEnd()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIRunEnd();
    }

    private void Credits()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UICredits();
    }

    private void Upgrades()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIUpgrades();
    }

    private void RunWin()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIRunWin();
    }

    private void Intro()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIIntro();
    }

    private void Controls()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIControls();
    }

    #endregion

    #region State Changers
    public void PauseGame()
    {
        if (gameState != GameState.Pause && gameState == GameState.Gameplay)
        {
            previousGameState = gameState;
            gameState = GameState.Pause;
            Time.timeScale = 0;
            isSpedUp = false;
        }
        else if (gameState == GameState.Pause || gameState == GameState.Settings)
        {
            gameState = previousGameState;
            previousGameState = GameState.Pause;
            Time.timeScale = 1;
            isSpedUp = false;
            spin.isActive = true;
            stab.isActive = true;
            foreach (Combatant combatant in combatManager.combatants)
            {
                combatant.UnpauseTimer();
            }
            stab.selectText.SetActive(false);
            stab.timersPaused = false;
            spin.timersPaused = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        gameState = GameState.MainMenu;
    }

    public void OpenSettings()
    {
        if (gameState != GameState.Pause)
        {
            previousGameState = gameState;
        }
        gameState = GameState.Settings;
    }

    public void OpenCredits()
    {
        previousGameState = gameState;
        gameState = GameState.Credits;
    }

    public void OpenUpgrades()
    {
        previousGameState = gameState;
        gameState = GameState.Upgrades;
    }

    public void OpenIntro()
    {
        previousGameState = gameState;
        gameState = GameState.Intro;
    }

    public void OpenControls()
    {
        previousGameState = gameState;
        gameState = GameState.Controls;
    }

    public void Back()
    {
        if (gameState == GameState.Upgrades || gameState == GameState.Credits)
        {
            gameState = GameState.MainMenu;
        }
        else if (previousGameState == GameState.Upgrades)
        {
            gameState = GameState.Upgrades;
        }
        else if (previousGameState != GameState.MainMenu)
        {
            gameState = GameState.Pause;
            Debug.Log("backed");
        }
        else
        {
            gameState = previousGameState;
            Time.timeScale = 1;
            isSpedUp = false;
        }
    }
    #endregion

    public void MovePlayerToSpawnPoint()
    {
        player.transform.position = spawnPoint.transform.position;
        Time.timeScale = 1;
        isSpedUp = false;
    }

    public void ToggleGameSpeedUp()
    {
        if (isSpedUp)
        {
            Time.timeScale = 1;
            isSpedUp = false;
        }
        else
        {
            Time.timeScale = 3;
            isSpedUp = true;
        }
    }

    public void HandleSpeedupButtonChanges()
    {
        if (isSpedUp)
        {
            speedupImage.sprite = normalSpeedSprite;
        }
        if (!isSpedUp)
        {
            speedupImage.sprite = speedupSprite;
        }
    }

    public void ResetValues()
    {
        gold = defaultGold;
        damage = defaultDamage;
        maxHealth = defaultMaxHealth;
        attackSpeed = defaultAttackSpeed;

        spinAttack = defaultSpinAttack;
        stabAttack = defaultStabAttack;

        healing = defaultHealing;
        stabDamage = defaultStabDamage;

        numTargets = defaultNumTargets;
        bursts = defaultBursts;
    }

    public void PlayGame()
    {
        if (File.Exists(saveFilePath))
        {
            uiManager.OpenSelectionScreen();
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        saveManager.DeleteSave();
        OpenIntro();
        uiManager.choicePrompt.SetActive(false);
    }

    public void LoadGame()
    {
        saveManager.Load();
        OpenUpgrades();
        uiManager.choicePrompt.SetActive(false);
    }
}