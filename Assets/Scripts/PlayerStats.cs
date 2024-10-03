using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    //this will be where all the stats for the player are stored and will be called to use and will be affected by the upgrades.
    public static int maxHealth = 100;
    public static int damage = 100; // affects the damage (%)
    public static int attackSpeed = 100; // affects the cooldowns (%)
    public static int currency = 0; // used to purchase upgrades
}

[Serializable]
public class PlayerSkills
{
    public bool spinAttack = false;
    public bool largeStab = false;
}
