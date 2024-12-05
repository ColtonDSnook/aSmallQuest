using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject PauseUI;
    public GameObject GameplayUI;
    public GameObject SettingsUI;
    public GameObject RunEndUI;
    public GameObject CreditsUI;
    public GameObject UpgradesUI;
    public GameObject RunWinUI;
    public GameObject IntroUI;
    public GameObject ControlsUI;

    public GameManager gameManager;
    public CombatManager combatManager;

    public GameObject choicePrompt;

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI upgradesGoldText;
    public TextMeshProUGUI healthText;

    public void UIMainMenu()
    {
        MainMenuUI.SetActive(true);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        RunWinUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIPause()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(true);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        GameplayUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIGameplay()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(true);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UISettings()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(true);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIRunEnd()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunEndUI.SetActive(true);
        RunWinUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UICredits()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(true);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIRunWin()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(true);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIUpgrades()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(true);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    public void UIIntro()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(true);
        ControlsUI.SetActive(false);
    }

    public void UIControls()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        CreditsUI.SetActive(false);
        UpgradesUI.SetActive(false);
        RunWinUI.SetActive(false);
        RunEndUI.SetActive(false);
        IntroUI.SetActive(false);
        ControlsUI.SetActive(true);
    }

    public void OpenSelectionScreen()
    {
        choicePrompt.SetActive(true);
    }


    public void UpdateText()
    {
        damageText.text = ": " + gameManager.damage.ToString();
        speedText.text = ": " + gameManager.attackSpeed.ToString();
        upgradesGoldText.text = ": " + gameManager.gold.ToString();
        healthText.text = ": " + gameManager.maxHealth.ToString();

        goldText.text = combatManager.coinsGainedCurrentRun.ToString();
    }
}
