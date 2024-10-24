using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class FollowScript : MonoBehaviour
{
    [SerializeField] private float distance = cameraDistance;
    [SerializeField] private float height = cameraHeight;
    [SerializeField] private float rotation = cameraRotation;
    [SerializeField] private float offset = cameraOffset;

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
