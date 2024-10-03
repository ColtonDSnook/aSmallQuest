using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        if (tag == "Enemy")
        {
            currentHealth = 1;
        }
        else
        {
            currentHealth = PlayerStats.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + GetCurrentHealth().ToString();
        GetCurrentHealth();
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
