using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;

    public CombatManager combatManager;
    public GameManager gameManager;

    public GameObject abilityUI;

    public Combatant player;

    public Image ability;
    public Image abilityRadial;
    public Sprite abilityColor;
    public Sprite abilityGrey;

    public TextMeshProUGUI timerText;

    public float timeRemaining;
    public float maxCountDownTime;

    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "";
        ability = GetComponent<Image>();

        gameManager = FindObjectOfType<GameManager>();
        combatManager = FindObjectOfType<CombatManager>();

        RefreshAbility();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            if (timeRemaining < 1)
            {
                timerText.text = timeRemaining.ToString("N1");
                timeRemaining -= Time.deltaTime;
                abilityRadial.fillAmount = timeRemaining / maxCountDownTime;
            }
            else
            {
                timerText.text = timeRemaining.ToString("N0");
                timeRemaining -= Time.deltaTime;
                abilityRadial.fillAmount = timeRemaining / maxCountDownTime;
            }
        }
        else
        {
            RefreshAbility();
        }
    }

    public void RefreshAbility()
    {
        ability.sprite = abilityColor;
        timerText.text = "";
        timeRemaining = 0;
        abilityRadial.fillAmount = 0;
    }

    public abstract void UseAbility();
}
