using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Ability : MonoBehaviour
{
    //public bool reactivate;

    //float reactivateTimer = 1;

    //bool recast = false;

    Image ability;
    public Image abilityRadial;
    public Sprite[] abilitiesColor;
    Sprite abilityColor;
    Sprite abilityGrey;
    public Sprite[] abilitiesGrey;

    //public TMP_InputField chatBox;

    public TextMeshProUGUI timerText;

    float timeRemaining;
    public float maxCountDownTime;

    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        abilityColor = abilitiesColor[0];
        abilityGrey = abilitiesGrey[0];

        timerText.text = "";
        ability = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!chatBox.isFocused)
        //{
        //    if (Input.GetKeyDown(key) && timeRemaining == 0)
        //    {
        //        UseAbility();
        //    }
        //}


        //if (recast)
        //{
        //    abilityColor = abilitiesColor[1];
        //    abilityGrey = abilitiesGrey[1];
        //}
        //else
        //{
        //    abilityColor = abilitiesColor[0];
        //    abilityGrey = abilitiesGrey[0];
        //}

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
    }

    public void UseAbility()
    {
        if (timeRemaining == 0)
        {
            ability.sprite = abilityGrey;
            abilityRadial.fillAmount = 1;
            timeRemaining = maxCountDownTime;
        }
    }
}
