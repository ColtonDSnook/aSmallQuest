using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public int distance;
    public int height;
    public int rotation;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(0, height, distance);
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }
}
