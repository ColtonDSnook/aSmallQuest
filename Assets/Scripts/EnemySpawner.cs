using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int minTime;
    public int maxTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Debug.Log("Cooldown Over");
        yield return null;
    }
}
