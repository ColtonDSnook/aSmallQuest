using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public UpgradeManager upgradeManager;

    private UIManager uiManager;
    private PlayerMovement player;
    public GameObject playerSprite;
    public GameObject spawnPoint;

    public PlayerStats playerStats;

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI goldText;

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
        Intro
    }

    public GameState previousGameState;
    public GameState gameState;

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
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

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
        }
    }

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
        playerSprite.SetActive(false);
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

    public void PauseGame()
    {
        if (gameState != GameState.Pause && gameState == GameState.Gameplay)
        {
            previousGameState = gameState;
            gameState = GameState.Pause;
            Time.timeScale = 0;
        }
        else if (gameState == GameState.Pause || gameState == GameState.Settings)
        {
            gameState = previousGameState;
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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
        gameState = GameState.Credits;
    }

    public void OpenUpgrades()
    {
        gameState = GameState.Upgrades;
    }

    public void OpenIntro()
    {
        gameState = GameState.Intro;
    }

    public void MovePlayerToSpawnPoint()
    {
        player.transform.position = spawnPoint.transform.position;
        Time.timeScale = 1;
    }

    public void Back()
    {
        if (gameState == GameState.Upgrades)
        {
            gameState = GameState.MainMenu;
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
        }
    }

    public void UpdateText()
    {
        damageText.text = "DMG: " + playerStats.damage.ToString();
        speedText.text = "SPD: " + playerStats.attackSpeed.ToString();
        goldText.text = "GLD: " + playerStats.currency.ToString();
    }

    public void UpdatePlayerStats(PlayerStats updatedStats)
    {
        playerStats = updatedStats;
        UpdateText();
    }

}
