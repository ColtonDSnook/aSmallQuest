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

    public enum TriggerType
    {
        Spawning,
        Combat
    }

    public TriggerType triggerType;

    private void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (triggerType == TriggerType.Spawning)
            {
                AddPositions();
                SpawnEnemies();
            }
            else if (triggerType == TriggerType.Combat)
            {
                combatManager.combatState = CombatManager.CombatState.Start;
            }
            this.enabled = false;
        }
    }

    void AddPositions()
    {
        Vector3 offset = new Vector3(19, 0, 0); // calculate this differently later.

        positions.Clear();
        positions.Add(player.transform.position + offset + new Vector3(5.13f, height, 0));
        positions.Add(player.transform.position + offset + new Vector3(7.04f, height, -2.74f));
        positions.Add(player.transform.position + offset + new Vector3(7.84f, height, 2.74f));
        positions.Add(player.transform.position + offset + new Vector3(3.28f, height, -3.84f));
        positions.Add(player.transform.position + offset + new Vector3(3.14f, height, 4.5f));
    }


    void SpawnEnemies()
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
