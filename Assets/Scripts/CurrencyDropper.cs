using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class CurrencyDropper : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameManager gameManager;
    public int DropCurrency(int min = minGoldDropped, int max = maxGoldDropped)
    {
        //return Random.Range(15, 25);

        int randomInt = Random.Range(min, max);

        //gameManager.UpdatePlayerStats(playerStats);

        gameManager.gold += randomInt;

        return randomInt;
        //this will be called when the enemy dies and will drop currency for the player to pick up.
        // we could either return the value or add it to the player's currency directly.
    }
}
