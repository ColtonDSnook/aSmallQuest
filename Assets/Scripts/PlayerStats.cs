using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class PlayerStats
{
    //this will be where all the stats for the player are stored and will be called to use and will be affected by the upgrades.

    public static int damage; // affects the damage
    public static int attackSpeed; // affects the cooldowns
    public static int currency; // used to purchase upgrades
}
