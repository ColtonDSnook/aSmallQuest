using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    private string saveFilePath;
    public GameManager gameManager;
    public UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";

        //if (playerStats == null)
        //    playerStats = new PlayerStats();

        //if (playerSkills == null)
        //    playerSkills = new PlayerSkills();


        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";

        if (File.Exists(saveFilePath))
        {
            Load();
        }
        else
        {
            upgradeManager.InitializeUpgrades();
        }

    }

    // Save player data to a file
    public void Save()
    {
        // Create a BinaryFormatter and a FileStream
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        // Create a new PlayerData object and set its properties
        SaveData saveData = new SaveData();
        saveData.playerStats.damage = gameManager.damage;
        saveData.playerStats.maxHealth = gameManager.maxHealth;
        saveData.playerStats.attackSpeed = gameManager.attackSpeed;
        saveData.playerStats.currency = gameManager.gold;
        //saveData.playerSkills = playerSkills;
        saveData.upgrades = upgradeManager.upgrades;

        Debug.Log("Saving: Health - " + saveData.playerStats.maxHealth + ", Damage - " + saveData.playerStats.damage + ", Gold - " + saveData.playerStats.currency);
        //gameManager.UpdatePlayerStats(playerStats);
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            //Debug.Log("Loaded Attack: " + saveData.playerStats.GetStat("Damage"));

            //playerSkills = saveData.playerSkills;
            gameManager.damage = saveData.playerStats.damage;
            gameManager.maxHealth = saveData.playerStats.maxHealth;
            gameManager.attackSpeed = saveData.playerStats.attackSpeed;
            gameManager.gold = saveData.playerStats.currency;
            upgradeManager.upgrades = saveData.upgrades;
            Debug.Log("Loading Game");
            //Debug.Log("Loaded: Health - " + playerStats.maxHealth + ", Damage - " + playerStats.damage + ", Gold - " + saveData.playerStats.currency);
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }
}
