using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int upgradeIndex; // The index of the upgrade this button applies
    public UpgradeManager upgradeManager;

    public void OnButtonClick()
    {
        if (upgradeManager.upgrades[upgradeIndex].isPurchased)
        {
            Debug.Log("Cannot Repurchase");
        }
        else
        {
            upgradeManager.ShowUpgradeDescription(upgradeIndex);
            Debug.Log("Clicked");
        }
    }
}
