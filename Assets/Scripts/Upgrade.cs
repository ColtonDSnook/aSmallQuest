using System;

[Serializable]
public class Upgrade
{
    public enum UpgradeType 
    {
        Stat,
        Skill
    }

    public enum StatType
    {
        Health,
        Damage,
        AttackSpeed,
        Defense
    }

    public string upgradeName;
    public int cost;
    public bool isPurchased;
    public string description;

    public UpgradeType upgradeType;

    //if it is a stat upgrade
    public StatType statType;
    public float value;

    //if it is a skill upgrade
    public string skillName;

    public Upgrade(string name, string desc, StatType stat, float val)
    {
        upgradeName = name;
        description = desc;
        upgradeType = UpgradeType.Stat;
        statType = stat;
        value = val;
        isPurchased = false;
    }

    // Constructor for skill unlock
    public Upgrade(string name, string desc, string skill)
    {
        upgradeName = name;
        description = desc;
        upgradeType = UpgradeType.Skill;
        skillName = skill;
        isPurchased = false;
    }
}
