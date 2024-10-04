using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    public TextMeshProUGUI healthText;

    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        if (tag == "Enemy")
        {
            currentHealth = 1;
        }
        else
        {
            currentHealth = playerStats.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (CompareTag("Player"))
        {
            healthText.text = "HP: " + GetCurrentHealth().ToString();
        }

        if (healthText == null)
        {
            return;
        }
        //GetCurrentHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
