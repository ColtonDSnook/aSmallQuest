using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    // Player Variables
    public const int defaultPlayerCooldown = 2;

    public const float defaultGold = 0;
    public const float defaultMaxHealth = 100;
    public const float defaultDamage = 10;
    public const float defaultAttackSpeed = 1;

    public const bool defaultSpinAttack = false;
    public const float defaultNumTargets = 2;
    public const float defaultBursts = 1;

    public const bool defaultStabAttack = false;
    public const float defaultStabDamage = 10;
    public const float defaultHealing = 0;

    public const int spinAttackBaseDamage = 2;

    // Enemies Variables
    public const float defaultSlimeHealth = 20;
    public const float defaultSlimeCooldown = 5;
    public const float defaultSlimeDamage = 4;

    public const float defaultGoblinHealth = 50;
    public const float defaultGoblinCooldown = 4;
    public const float defaultGoblinDamage = 5;

    public const float defaultKoboldHealth = 60;
    public const float defaultKoboldCooldown = 7;
    public const float defaultKoboldDamage = 8;

    public const float defaultDungeonMasterHealth = 150;
    public const float defaultDungeonMasterCooldown = 10;
    public const float defaultDungeonMasterDamage = 30;

    // Encounters
    public const int defaultEncountersRequired = 2;

    // Gold Dropping
    public const int minGoldDropped = 15;
    public const int maxGoldDropped = 25;

    // Camera
    public const float cameraOffset = 4.5f;
    public const float cameraDistance = -9;
    public const float cameraRotation = 35;
    public const float cameraHeight = 8;
}
