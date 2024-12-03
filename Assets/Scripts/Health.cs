using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float currentHealth;
    public TextMeshProUGUI damageText;

    public float maxHealth;

    public Image healthBar;
    public PlayerStats playerStats;
    public Combatant combatant;
    public RectTransform damageNumber;


    // Start is called before the first frame update
    void Start()
    {
        damageText.gameObject.SetActive(false);
        combatant = GetComponent<Combatant>();
        damageText.text = "";
        if (CompareTag("Enemy"))
        {
            animator = GetComponentInChildren<Animator>();

            switch(combatant.combatantType)
            {
                case Combatant.CombatantType.Slime:
                    currentHealth = defaultSlimeHealth;
                    maxHealth = currentHealth;
                    break;
                case Combatant.CombatantType.Goblin:
                    currentHealth = defaultGoblinHealth;
                    maxHealth = currentHealth;
                    break;
                case Combatant.CombatantType.Kobold:
                    currentHealth = defaultKoboldHealth;
                    maxHealth = currentHealth;
                    break;
                case Combatant.CombatantType.DungeonMaster:
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
            maxHealth = GameManager.manager.maxHealth;
            healthBar.fillAmount = (float)GetCurrentHealth() / GameManager.manager.maxHealth;
        }

        else
        {
            healthBar.fillAmount = (float)GetCurrentHealth() / maxHealth;
        }
        //GetCurrentHealth();
    }

    public float TakeDamage(float damage)
    {
        float startingHealth = currentHealth;
        if (CompareTag("Enemy"))
        {
            animator.Play(combatant.animPrefix + "_Hurt");
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
        damageText.gameObject.SetActive(true);
        //Debug.Log("showing damage numbers");
        damageText.text = damage.ToString();
        damageNumber.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.25f).From(1.0f);
        damageNumber.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f).From(1.4f);
        yield return new WaitForSeconds(1.0f);
        damageText.text = "";
        damageText.gameObject.SetActive(false);
    }
}
