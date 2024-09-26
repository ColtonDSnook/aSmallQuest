using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDropper : MonoBehaviour
{
    public int DropCurrency()
    {
        return Random.Range(15, 25);
        //this will be called when the enemy dies and will drop currency for the player to pick up.
    }
}
