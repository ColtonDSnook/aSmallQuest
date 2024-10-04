using System;

[Serializable]
public class PlayerStats
{
    //this will be where all the stats for the player are stored and will be called to use and will be affected by the upgrades.
    public int maxHealth = 100;
    public int damage = 100; // affects the damage (%)
    public int attackSpeed = 100; // affects the cooldowns (%)
    public int currency = 0; // used to purchase upgrades

    public int GetStat(string stat)
    {
        switch(stat)
        {
            case "Health":
                return maxHealth;
            case "Damage":
                return damage;
            case "Speed":
                return attackSpeed;
            case "Gold":
                return currency;
        }
        return 0;
    }
}

[Serializable]
public class PlayerSkills
{
    public bool spinAttack = false;
    public bool largeStab = false;
}
