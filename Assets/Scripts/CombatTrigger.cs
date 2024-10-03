using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public CombatManager combatManager;

    public Combatant player;

    public GameObject enemyPrefab;

    public float height = 0.5f;

    public List<Vector3> positions;

    private void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        AddPositions();
    }

    private void OnTriggerEnter(Collider other)
    {
        InstantiateEnemies();

        combatManager.combatants.Add(player);

        combatManager.combatState = CombatManager.CombatState.InCombat;

        Debug.Log("In Combat");
    }

    void AddPositions()
    {
        positions.Add(player.transform.position + new Vector3(5.13f, height, 0));
        positions.Add(player.transform.position + new Vector3(7.04f, height, -2.74f));
        positions.Add(player.transform.position + new Vector3(7.84f, height, 2.74f));
        positions.Add(player.transform.position + new Vector3(3.28f, height, -4.41f));
        positions.Add(player.transform.position + new Vector3(3.14f, height, 4.5f));
    }

    void InstantiateEnemies()
    {
        int numEnemies = Random.Range(1,5);

        for (int i = 1; i == numEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, positions[i], Quaternion.identity);

            Combatant enemyCombatant = enemy.GetComponent<Combatant>();

            combatManager.combatants.Add(enemyCombatant);
        }
    }

}
