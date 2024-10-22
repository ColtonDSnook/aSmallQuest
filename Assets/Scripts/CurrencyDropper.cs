using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDropper : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameManager gameManager;
    public int DropCurrency(int min = 15, int max = 25)
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
