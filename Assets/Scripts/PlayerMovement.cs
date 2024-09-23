using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private bool inCombat;

    // Start is called before the first frame update
    void Start()
    {
        inCombat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            //stop moving
        }
        // the player move script will move the player forward and stop it when it encounters the enemy.
        // the player will not be able to move the player character manually
    }
}
