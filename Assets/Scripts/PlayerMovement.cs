using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    public CombatManager combatManager;

    //[SerializeField] private bool inCombat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (combatManager.combatState == CombatManager.CombatState.InCombat)
        {
            rb.velocity = Vector3.zero;
        }
        else if (combatManager.combatState == CombatManager.CombatState.None || combatManager.combatState == CombatManager.CombatState.Won)
        {
            rb.velocity = new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            //rb.AddForce(new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);
        }
        // the player move script will move the player forward and stop it when it encounters the enemy.
        // the player will not be able to move the player character manually
    }
}
