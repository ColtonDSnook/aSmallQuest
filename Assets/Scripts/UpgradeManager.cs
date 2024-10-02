using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Upgrade;

public class UpgradeManager : MonoBehaviour
{
    // this is where all the upgrades are stored and saved.
    // i will be saving these to a file sometime

    // these upgrades will directly affect the stats of the player.

    public PlayerStats playerStats;
    public PlayerSkills playerSkills;

    public List<Upgrade> upgrades;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUpgrades(); 
    }
    
    public void ApplyUpgrade(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex < upgrades.Count)
        {
            Upgrade upgrade = upgrades[upgradeIndex];

            switch (upgrade.upgradeType)
            {
                case Upgrade.UpgradeType.Stat:
                    ApplyStatUpgrade(upgrade);
                    break;
                case Upgrade.UpgradeType.Skill:
                    ApplySkillUpgrade(upgrade);
                    break;
            }
        }
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        switch (upgrade.statType)
        {
            case StatType.Health:
                PlayerStats.maxHealth += upgrade.value;
                break;
            case StatType.Damage:
                PlayerStats.damage += upgrade.value;
                break;
            case StatType.AttackSpeed:
                PlayerStats.attackSpeed += upgrade.value;
                break;
            //case StatType.Defense:
            //    playerStats.defense += upgrade.value;
            //    break;
        }
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
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 5", StatType.Damage, 5));
        upgrades.Add(new Upgrade("Unlock Spin Attack", "Unlock the Spin Attack skill", "SpinAttack"));
        upgrades.Add(new Upgrade("Damage Boost", "Increase attack power by 1", StatType.Damage, 1));
    }
}
