using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // this is where all the upgrades are stored and saved.
    // i will be saving these to a file sometime(when i know how)((tomorrow))

    // these upgrades will directly affect the stats of the player.

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
