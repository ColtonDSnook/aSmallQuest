using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class Health : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float currentHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;

    public float maxHealth;

    public Image healthBar;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        damageText.text = "";
        if (CompareTag("Enemy"))
        {
            animator = GetComponentInChildren<Animator>();

            switch(name)
            {
                case "Slime(Clone)":
                    currentHealth = defaultSlimeHealth;
                    maxHealth = currentHealth;
                    break;
                case "Goblin(Clone)":
                    currentHealth = defaultGoblinHealth;
                    maxHealth = currentHealth;
                    break;
                case "Kobold(Clone)":
                    currentHealth = defaultKoboldHealth;
                    maxHealth = currentHealth;
                    break;
                case "DungeonMaster(Clone)":
                    currentHealth = defaultDungeonMasterHealth;
                    maxHealth = currentHealth;
                    break;
            }
        }
        else
        {
            currentHealth = GameManager.manager.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Player"))
        {
            healthBar.fillAmount = (float)GetCurrentHealth() / GameManager.manager.maxHealth;
        }

        else
        {
            healthBar.fillAmount = (float)GetCurrentHealth() / maxHealth;
        }

        if (healthText == null)
        {
            return;
        }
        //GetCurrentHealth();
    }

    public float TakeDamage(float damage)
    {
        float startingHealth = currentHealth;
        if (CompareTag("Enemy"))
        {
            //animator.Play("Slime_Hurt");
            if (damage > maxHealth)
            {
                currentHealth = 0;
                StartCoroutine(ShowDamageNumbers(startingHealth - currentHealth));
                return startingHealth - currentHealth;
            }
            else
            {
                currentHealth -= damage;
                StartCoroutine(ShowDamageNumbers(startingHealth - currentHealth));
                return startingHealth - currentHealth;
            }

        }
        else if (CompareTag("Player"))
        {
            currentHealth -= damage;
            animator.Play("MC_Hurt");
            StartCoroutine(ShowDamageNumbers(startingHealth - currentHealth));
            return startingHealth - currentHealth;
        }
        return 0;
    }

    public void Heal(float healing)
    {
        if ((currentHealth += healing) > GameManager.manager.maxHealth)
        {
            currentHealth = GameManager.manager.maxHealth;
        }
        else
        {
            currentHealth += healing;
        }

        Debug.Log(gameObject.name + " Has healed " + healing);
        
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth()
    {
        currentHealth = GameManager.manager.maxHealth;
    }

    public IEnumerator ShowDamageNumbers(float damage)
    {
        //Debug.Log("showing damage numbers");
        damageText.text = damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.text = "";
    }
}
