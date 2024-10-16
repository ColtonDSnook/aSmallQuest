using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    CombatManager combatManager;
    public string previousScene;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string levelName)
    {
        Debug.Log("Level Loaded: " + levelName);
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (levelName.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
        }
        
        if (levelName == "Post-Run")
        {
            if (gameManager.gameState == GameManager.GameState.Pause)
            {
                gameManager.gameState = GameManager.GameState.RunEnd;
                Time.timeScale = 1;
            }

            if (combatManager.previousCombatState == CombatManager.CombatState.Won)
            {
                gameManager.gameState = GameManager.GameState.RunWin;
            }
            else if (combatManager.previousCombatState == CombatManager.CombatState.Lost)
            {
                gameManager.gameState = GameManager.GameState.RunEnd;
            }
        }
        if (levelName == "Settings")
        {
            gameManager.gameState = GameManager.GameState.Settings;
        }
        if (levelName == "Main Menu")
        {
            Time.timeScale = 1;
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        SceneManager.LoadScene(levelName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameManager.playerSprite.activeSelf == true)
        {
            gameManager.spawnPoint = GameObject.FindWithTag("SpawnPoint");
            gameManager.MovePlayerToSpawnPoint();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadPreviousScene()
    {
        SceneManager.sceneLoaded += OnPreviousSceneLoaded;
        if (previousScene.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
            Time.timeScale = 1;
        }
        if (previousScene == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        SceneManager.LoadScene(previousScene);
    }

    private void OnPreviousSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //gameManager.MovePlayerToPreviousLocation();
        SceneManager.sceneLoaded -= OnPreviousSceneLoaded;
    }
}
