using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    // Player Variables
    public const int defaultPlayerCooldown = 2; // seconds

    public const float defaultGold = 0; // g
    public const float defaultMaxHealth = 100; // hp
    public const float defaultDamage = 10; // #
    public const float defaultAttackSpeed = 1; // %

    public const bool defaultSpinAttack = false;
    public const float defaultNumTargets = 2; // #
    public const float defaultBursts = 1; // #

    public const bool defaultStabAttack = false;
    public const float defaultStabDamage = 3; // %
    public const float defaultHealing = 0; // %

    public const float spinAttackBaseDamage = 0.5f; // 50%

    // Enemies Variables
    public const float defaultSlimeHealth = 25;
    public const float defaultSlimeDamage = 5;
    public const float defaultSlimeAttackSpeed = 0.5f;
    public const float defaultSlimeAttackAnimTime = 1f;
    public const string slimeAnimPrefix = "Slime";

    public const float defaultGoblinHealth = 40;
    public const float defaultGoblinDamage = 10;
    public const float defaultGoblinAttackSpeed = 0.75f;
    public const float defaultGoblinAttackAnimTime = 0.5f;
    public const string goblinAnimPrefix = "Goblin";

    public const float defaultKoboldHealth = 70;
    public const float defaultKoboldDamage = 20;
    public const float defaultKoboldAttackSpeed = 0.3f;
    public const float defaultKoboldAttackAnimTime = 0.5f;
    public const string koboldAnimPrefix = "Kobold";

    public const float defaultDungeonMasterHealth = 600;
    public const float defaultDungeonMasterDamage = 25;
    public const float defaultDungeonMasterAttackSpeed = 0.75f;
    public const float defaultDungeonMasterAttackAnimTime = 1f;
    public const string dungeonMasterAnimPrefix = "DM";

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
