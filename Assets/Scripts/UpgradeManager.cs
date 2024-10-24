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
    public TextMeshProUGUI costText;
    public Button confirmButton;

    private int selectedUpgradeIndex = -1;

    //private string saveFilePath;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        //if (playerStats == null)
        //    playerStats = new PlayerStats();

        //if (playerSkills == null)
        //    playerSkills = new PlayerSkills();


        //saveFilePath = Application.persistentDataPath + "/playerInfo.dat";

        descriptionUI.SetActive(false);
    }

    private void Update()
    {
        //Debug.LogWarning("Gold: " + playerStats.currency);
    }

    public void ShowUpgradeDescription(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Count)
        {
            Upgrade upgrade = upgrades[upgradeIndex];
            descriptionText.text = upgrade.description;
            costText.text = upgrade.cost.ToString() + "g";
            selectedUpgradeIndex = upgradeIndex;
            descriptionUI.SetActive(true);
        }
    }

    public void ConfirmUpgrade()
    {
        if (selectedUpgradeIndex >= 0 && selectedUpgradeIndex < upgrades.Count)
        {
            Upgrade upgrade = upgrades[selectedUpgradeIndex];
            if (gameManager.gold >= upgrade.cost)
            {
                switch (upgrade.upgradeType)
                {
                    case UpgradeType.Stat:
                        ApplyStatUpgrade(upgrade);
                        gameManager.gold -= upgrade.cost;
                        //Debug.Log("upgraded" + upgrade.description);
                        break;
                    case UpgradeType.Skill:
                        ApplySkillUpgrade(upgrade);
                        gameManager.gold -= upgrade.cost;
                        break;
                }

                upgrade.isPurchased = true;
                gameManager.Save();
                descriptionText.text = "";
                descriptionUI.SetActive(false);
            }
            else
            {
                Debug.Log("Cannot Afford Upgrade");
            }

        }
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        switch (upgrade.statType)
        {
            case StatType.Health:
                gameManager.maxHealth += upgrade.value;
                break;
            case StatType.Damage:
                Debug.Log(playerStats.damage);
                gameManager.damage += upgrade.value;
                Debug.Log(playerStats.damage);
                break;
            case StatType.AttackSpeed:
                gameManager.attackSpeed += upgrade.value;
                break;
            case StatType.NumTargets:
                gameManager.numTargets += upgrade.value;
                break;
            case StatType.Bursts:
                gameManager.bursts += upgrade.value;
                break;
            case StatType.Healing:
                gameManager.healing += upgrade.value;
                break;
            case StatType.StabDamage:
                gameManager.stabDamage += upgrade.value;
                break;
            //case StatType.Defense:
            //    playerStats.defense += upgrade.value;
            //    break;
        }
        gameManager.Save();
    }

    public void ApplySkillUpgrade(Upgrade upgrade)
    {
        switch (upgrade.skillName)
        {
            case "SpinAttack":
                gameManager.spinAttack = true;
                break;
            case "LargeStab":
                gameManager.stabAttack = true;
                break;
        }
    }

    public void InitializeUpgrades()
    {
        Debug.Log("initialized upgrades");

        // Player Upgrades
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20, 20)); // 0
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20, 40)); // 1
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20, 60)); // 2
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20, 80)); // 3
        upgrades.Add(new Upgrade("Health Boost", "Increase max health by 20", StatType.Health, 20, 100)); // 4
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 2", StatType.Damage, 2, 20)); // 5
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 2", StatType.Damage, 2, 40)); // 6
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 2", StatType.Damage, 2, 60)); // 7
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 2", StatType.Damage, 2, 80)); // 8
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 2", StatType.Damage, 2, 100)); // 9
        upgrades.Add(new Upgrade("Attack Speed Boost", "Increase attack speed by 20%", StatType.AttackSpeed, 0.2f, 20)); // 20%  10
        upgrades.Add(new Upgrade("Attack Speed Boost", "Increase attack speed by 20%", StatType.AttackSpeed, 0.2f, 40)); // 20%  11
        upgrades.Add(new Upgrade("Attack Speed Boost", "Increase attack speed by 20%", StatType.AttackSpeed, 0.2f, 60)); // 20%  12
        upgrades.Add(new Upgrade("Attack Speed Boost", "Increase attack speed by 20%", StatType.AttackSpeed, 0.2f, 80)); // 20%  13
        upgrades.Add(new Upgrade("Attack Speed Boost", "Increase attack speed by 20%", StatType.AttackSpeed, 0.2f, 100)); // 20%  14

        // Ability unlocks
        upgrades.Add(new Upgrade("Unlock Spin Attack", "Unlock the Spin Attack skill", "SpinAttack", 100)); // 15
        upgrades.Add(new Upgrade("Unlock Large Stab Attack", "Unlock the Large Stab Attack skill", "LargeStab", 100)); // 16

        // Spin Attack upgrades
        upgrades.Add(new Upgrade("Targets Increase", "Increase amount of targets attacked by the spin attack", StatType.NumTargets, 1, 150)); // 17
        upgrades.Add(new Upgrade("Targets Increase", "Increase amount of targets attacked by the spin attack", StatType.NumTargets, 1, 170)); // 18
        upgrades.Add(new Upgrade("Targets Increase", "Increase amount of targets attacked by the spin attack", StatType.NumTargets, 1, 200)); // 19
        upgrades.Add(new Upgrade("Bursts Increase", "Increase amount of bursts of damage done by the spin attack", StatType.Bursts, 1, 150)); // 20
        upgrades.Add(new Upgrade("Bursts Increase", "Increase amount of bursts of damage done by the spin attack", StatType.Bursts, 1, 170)); // 21
        upgrades.Add(new Upgrade("Bursts Increase", "Increase amount of bursts of damage done by the spin attack", StatType.Bursts, 1, 200)); // 22

        // Stab Attack upgrades
        upgrades.Add(new Upgrade("Healing", "Heals player for 10% of damage dealt by stab attack", StatType.Healing, 10, 150)); // 10%  23
        upgrades.Add(new Upgrade("Increase Healing", "Increase healing by 10%", StatType.Healing, 0.1f, 170)); // 10%  24
        upgrades.Add(new Upgrade("Increase Healing", "Increase healing by 10%", StatType.Healing, 0.1f, 200)); // 10%  25
        upgrades.Add(new Upgrade("Stab Damage Increase", "Increases stab damage by 200%", StatType.StabDamage, 2, 150)); // 200%  26
        upgrades.Add(new Upgrade("Stab Damage Increase", "Increases stab damage by 200%", StatType.StabDamage, 2, 170)); // 200%  27
        upgrades.Add(new Upgrade("Stab Damage Increase", "Increases stab damage by 200%", StatType.StabDamage, 2, 200)); // 200%  28

    }
}
