using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static Upgrade;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    // this is where all the upgrades are stored and saved.
    // i will be saving these to a file sometime

    // these upgrades will directly affect the stats of the player.

    public PlayerStats playerStats;
    public PlayerSkills playerSkills;

    public GameManager gameManager;

    public List<Upgrade> upgrades;

    public GameObject descriptionUI;
    public TextMeshProUGUI descriptionText;
    public Button confirmButton;

    private int selectedUpgradeIndex = -1;

    private string saveFilePath;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (playerStats == null)
            playerStats = new PlayerStats();

        if (playerSkills == null)
            playerSkills = new PlayerSkills();


        saveFilePath = Application.persistentDataPath + "/playerInfo.dat";
        
        if (File.Exists(saveFilePath))
        {
            Load();
        }
        else
        {
            InitializeUpgrades();
        }

        descriptionUI.SetActive(false);
    }

    public void ShowUpgradeDescription(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Count)
        {
            Upgrade upgrade = upgrades[upgradeIndex];
            descriptionText.text = upgrade.description;
            selectedUpgradeIndex = upgradeIndex;
            descriptionUI.SetActive(true);
        }
    }

    public void ConfirmUpgrade()
    {
        if (selectedUpgradeIndex >= 0 && selectedUpgradeIndex < upgrades.Count)
        {
            Upgrade upgrade = upgrades[selectedUpgradeIndex];

            switch (upgrade.upgradeType)
            {
                case UpgradeType.Stat:
                    ApplyStatUpgrade(upgrade);
                    Debug.Log("upgraded" + upgrade.description);
                    break;
                case UpgradeType.Skill:
                    ApplySkillUpgrade(upgrade);
                    break;
            }

            upgrade.isPurchased = true;
            Save();
            descriptionText.text = "";
            descriptionUI.SetActive(false);

        }
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        switch (upgrade.statType)
        {
            case StatType.Health:
                playerStats.maxHealth += upgrade.value;
                break;
            case StatType.Damage:
                Debug.Log(playerStats.damage);
                playerStats.damage += upgrade.value;
                Debug.Log(playerStats.damage);
                break;
            case StatType.AttackSpeed:
                playerStats.attackSpeed += upgrade.value;
                break;
            //case StatType.Defense:
            //    playerStats.defense += upgrade.value;
            //    break;
        }
        Save();
    }

    public void ApplySkillUpgrade(Upgrade upgrade)
    {
        switch (upgrade.skillName)
        {
            case "SpinAttack":
                playerSkills.spinAttack = true;
                break;
            case "LargeStab":
                playerSkills.largeStab = true;
                break;
        }
    }

    public void InitializeUpgrades()
    {
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20));
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 20%", StatType.Damage, 20));
        upgrades.Add(new Upgrade("Unlock Spin Attack", "Unlock the Spin Attack skill", "SpinAttack"));
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 20$", StatType.Damage, 20));
    }


    // Save player data to a file
    public void Save()
    {
        // Create a BinaryFormatter and a FileStream
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        // Create a new PlayerData object and set its properties
        SaveData saveData = new SaveData();
        saveData.playerStats = playerStats;
        saveData.playerSkills = playerSkills;
        saveData.upgrades = upgrades;

        Debug.Log("Saving: Health - " + saveData.playerStats.maxHealth + ", Damage - " + saveData.playerStats.damage);
        gameManager.UpdatePlayerStats(playerStats);
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

            playerSkills = saveData.playerSkills;
            playerStats = saveData.playerStats;
            upgrades = saveData.upgrades;
            Debug.Log("Loading Game");
            gameManager.UpdatePlayerStats(playerStats);
            //Debug.Log("Loaded: Health - " + playerStats.maxHealth + ", Damage - " + playerStats.damage);
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }
}
