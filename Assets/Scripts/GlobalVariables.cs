using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    // Player Variables
    public const int defaultPlayerCooldown = 3;

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
    public const float defaultSlimeHealth = 15;
    public const float defaultSlimeDamage = 2;
    public const float defaultSlimeAttackSpeed = 0.5f;

    public const float defaultGoblinHealth = 20;
    public const float defaultGoblinDamage = 3;
    public const float defaultGoblinAttackSpeed = 1;

    public const float defaultKoboldHealth = 30;
    public const float defaultKoboldDamage = 10;
    public const float defaultKoboldAttackSpeed = 0.5f;

    public const float defaultDungeonMasterHealth = 80;
    public const float defaultDungeonMasterDamage = 20;
    public const float defaultDungeonMasterAttackSpeed = 0.75f;

    // Encounters
    public const int defaultEncountersRequired = 31; // 31  (3 enemy types x 10 each + 1 boss)

    // Gold Dropping
    public const int minGoldDropped = 5;
    public const int maxGoldDropped = 15;

    // Camera
    public const float cameraOffset = 4.5f;
    public const float cameraDistance = -9;
    public const float cameraRotation = 35;
    public const float cameraHeight = 8;
}
