using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;

    //[SerializeField] private bool inCombat;
    public bool inCombat;

    // Start is called before the first frame update
    void Start()
    {
        inCombat = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!inCombat)
        {
            rb.AddForce(new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.AddForce(new Vector3(0, 0, 0));
        }
        // the player move script will move the player forward and stop it when it encounters the enemy.
        // the player will not be able to move the player character manually
    }
}
