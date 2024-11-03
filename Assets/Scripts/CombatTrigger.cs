using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public CombatManager combatManager;

    public GameObject player;

    public GameObject enemyPrefab;

    public float height = -2.5f;

    public List<Vector3> positions;

    private void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        AddPositions();
        Debug.Log(positions[1]);
        InstantiateEnemies();

        combatManager.combatState = CombatManager.CombatState.Start;
        Destroy(this);
        Debug.Log("In Combat");
    }

    void AddPositions()
    {
        positions.Add(player.transform.position + new Vector3(5.13f, height, 0));
        positions.Add(player.transform.position + new Vector3(7.04f, height, -2.74f));
        positions.Add(player.transform.position + new Vector3(7.84f, height, 2.74f));
        positions.Add(player.transform.position + new Vector3(3.28f, height, -3.84f));
        positions.Add(player.transform.position + new Vector3(3.14f, height, 4.5f));
    }

    void InstantiateEnemies()
    {
        int numEnemies;
        if (enemyPrefab.name == "DungeonMaster")
        {
            numEnemies = 1;
        }
        else
        {
            numEnemies = Random.Range(2, 5);
        }

        for (int i = 1; i <= numEnemies; i++)
        {
            Instantiate(enemyPrefab, positions[i], Quaternion.identity);
            //Combatant enemyCombatant = enemy.GetComponent<Combatant>();

            //combatManager.combatants.Add(enemyCombatant);
        }
    }

}
