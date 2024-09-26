using System.Collections;
using System.Collections.Generic;
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
    }
}
