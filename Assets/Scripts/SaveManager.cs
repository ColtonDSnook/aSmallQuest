using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private string saveFilePath;

    public GameManager gameManager;
    public UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";
        gameManager = FindObjectOfType<GameManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

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
                saveData.playerSkills = new PlayerSkills();
                saveData.playerStats.currency = gameManager.gold;
                saveData.playerStats.damage = gameManager.damage;
                saveData.playerStats.attackSpeed = gameManager.attackSpeed;
                saveData.playerStats.maxHealth = gameManager.maxHealth;
                saveData.playerStats.stabDamage = gameManager.stabDamage;
                saveData.playerStats.healing = gameManager.healing;
                saveData.playerStats.bursts = gameManager.bursts;
                saveData.playerStats.numTargets = gameManager.numTargets;
                saveData.playerSkills.spinAttack = gameManager.spinAttack;
                saveData.playerSkills.largeStab = gameManager.stabAttack;
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
                gameManager.stabAttack = saveData.playerSkills.largeStab;
                gameManager.spinAttack = saveData.playerSkills.spinAttack;
                gameManager.healing = saveData.playerStats.healing;
                gameManager.stabDamage = saveData.playerStats.stabDamage;
                gameManager.bursts = saveData.playerStats.bursts;
                gameManager.numTargets = saveData.playerStats.numTargets;
                gameManager.gold = saveData.playerStats.currency;
                gameManager.damage = saveData.playerStats.damage;
                gameManager.maxHealth = saveData.playerStats.maxHealth;
                gameManager.attackSpeed = saveData.playerStats.attackSpeed;
                gameManager.upgradeManager.upgrades = saveData.upgrades;

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
        }
        else
        {
            Debug.LogWarning("Save file failed to delete");
        }

        gameManager.ResetValues();

        upgradeManager.upgrades.Clear();
        upgradeManager.InitializeUpgrades();
    }
}
