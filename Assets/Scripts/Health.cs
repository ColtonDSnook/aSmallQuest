using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private int currentHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;

    public Image healthBar;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        damageText.text = "";
        if (CompareTag("Enemy"))
        {
            animator = GetComponentInChildren<Animator>();
            currentHealth = 10;
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
            healthBar.fillAmount = (float)GetCurrentHealth() / playerStats.maxHealth;
        }

        else
        {
            healthBar.fillAmount = (float)GetCurrentHealth() / 10;
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
        if (CompareTag("Enemy"))
        {
            animator.Play("Slime_Hurt");
        }
        else if (CompareTag("Player"))
        {
            animator.Play("MC_Hurt");
        }
        StartCoroutine(ShowDamageNumbers(damage));
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth()
    {
        currentHealth = playerStats.maxHealth;
    }

    public IEnumerator ShowDamageNumbers(int damage)
    {
        damageText.text = damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.text = "";
    }
}
