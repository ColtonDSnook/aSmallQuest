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
        NumTargets,
        StabDamage,
        Healing,
        Bursts
        //Defense
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

    // Constructor for stat unlock
    public Upgrade(string name, string desc, StatType stat, float val, int cost)
    {
        upgradeName = name;
        description = desc;
        upgradeType = UpgradeType.Stat;
        statType = stat;
        value = val;
        this.cost = cost;
        isPurchased = false;
    }

    // Constructor for skill unlock
    public Upgrade(string name, string desc, string skill, int cost)
    {
        upgradeName = name;
        description = desc;
        upgradeType = UpgradeType.Skill;
        skillName = skill;
        this.cost = cost;
        isPurchased = false;
    }
}
