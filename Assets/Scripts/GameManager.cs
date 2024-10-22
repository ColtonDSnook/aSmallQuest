using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public UpgradeManager upgradeManager;

    private UIManager uiManager;
    private PlayerMovement player;
    public GameObject playerSprite;
    public GameObject spawnPoint;

    //public PlayerStats playerStats;
    public PlayerSkills playerSkills;

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI goldText;

    public int gold = 0; //#
    public int damage = 10; //#
    public int maxHealth = 100; //#
    public int attackSpeed = 100; //%

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
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
        gameState = GameState.MainMenu;

        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";

        if (File.Exists(saveFilePath))
        {
            Load();
        }
        else
        {
            upgradeManager.InitializeUpgrades();

            //if (playerStats == null)
            //    playerStats = new PlayerStats();

            if (playerSkills == null)
                playerSkills = new PlayerSkills();
        }
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
        damageText.text = "DMG: " + damage.ToString();
        speedText.text = "SPD: " + attackSpeed.ToString();
        goldText.text = "GLD: " + gold.ToString();
    }

    //public void UpdatePlayerStats(PlayerStats updatedStats)
    //{
    //    playerStats = updatedStats;
    //    UpdateText();
    //}

    public void Save()
    {
        try
        {
            // Create a BinaryFormatter and use a 'using' statement to ensure the file is closed properly
            BinaryFormatter bf = new BinaryFormatter();

            // Ensure the file stream is closed properly after saving data
            using (FileStream file = File.Create(saveFilePath))
            {
                // Create a new SaveData object and set its properties
                SaveData saveData = new SaveData();
                saveData.playerStats = new PlayerStats(); // Ensure playerStats is initialized
                saveData.playerStats.currency = gold;
                saveData.playerStats.damage = damage;
                saveData.playerStats.attackSpeed = attackSpeed;
                saveData.playerStats.maxHealth = maxHealth;
                saveData.playerSkills = playerSkills;
                saveData.upgrades = upgradeManager.upgrades;

                Debug.Log("Saving: Health - " + saveData.playerStats.maxHealth + ", Damage - " + saveData.playerStats.damage + ", Gold - " + saveData.playerStats.currency);

                // Serialize the save data
                bf.Serialize(file, saveData);
            } // File is automatically closed here
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save data: " + ex.Message);
        }
    }

    public void Load()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);



            if (file.Length > 0)
            {
                SaveData saveData = (SaveData)bf.Deserialize(file);

                // Load the data from saveData
                playerSkills = saveData.playerSkills;
                gold = saveData.playerStats.currency;
                damage = saveData.playerStats.damage;
                maxHealth = saveData.playerStats.maxHealth;
                attackSpeed = saveData.playerStats.attackSpeed;
                upgradeManager.upgrades = saveData.upgrades;

                Debug.Log("Game Loaded Successfully");
            }
            else
            {
                Debug.LogError("The save file is empty. Unable to load data.");
            }

            file.Close();
            //Debug.Log("Loaded: Health - " + playerStats.maxHealth + ", Damage - " + playerStats.damage + ", Gold - " + saveData.playerStats.currency);
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            gold = 0;
            damage = 10;
            maxHealth = 100;
            attackSpeed = 100;

            upgradeManager.upgrades.Clear();
            upgradeManager.InitializeUpgrades();

            if (playerSkills == null)
                playerSkills = new PlayerSkills();
        }
        else
        {
            Debug.LogWarning("Save file failed to delete");
        }
    }

}
