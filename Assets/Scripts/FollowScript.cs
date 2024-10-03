using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public float distance;
    public float height;
    public float rotation;
    public float offset;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(offset, height, distance);
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }
}
