using System;

[Serializable]
public class PlayerStats
{
    //this will be where all the stats for the player are stored and will be called to use and will be affected by the upgrades.
    public float maxHealth;
    public float damage; // affects the damage (%)
    public float attackSpeed; // affects the cooldowns (%)
    public float currency; // used to purchase upgrades

    public float numTargets; // spin attack skill upgrade
    public float bursts; // spin attack skill upgrade

    public float healing; // stab attack skill upgrade
    public float stabDamage; // stab attack skill upgrade

    //public int GetStat(string stat)
    //{
    //    switch(stat)
    //    {
    //        case "Health":
    //            return maxHealth;
    //        case "Damage":
    //            return damage;
    //        case "Speed":
    //            return attackSpeed;
    //        case "Gold":
    //            return currency;
    //    }
    //    return 0;
    //}
}

[Serializable]
public class PlayerSkills
{
    public bool spinAttack = false;
    public bool largeStab = false;
}
