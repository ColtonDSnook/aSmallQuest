using System;

[Serializable]
public class PlayerStats
{
    //this will be where all the stats for the player are stored and will be called to use and will be affected by the upgrades.
    public int maxHealth;
    public int damage; // affects the damage (%)
    public int attackSpeed; // affects the cooldowns (%)
    public int currency; // used to purchase upgrades

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
