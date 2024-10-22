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

    private void Update()
    {
        // Ensure the upgradeIndex is within the bounds of the upgrades list
        if (upgradeIndex >= 0 && upgradeIndex < upgradeManager.upgrades.Count)
        {
            // Check if the upgrade is purchased
            if (upgradeManager.upgrades[upgradeIndex].isPurchased)
            {
                GetComponent<Image>().color = Color.gray;
            }
        }
        else
        {
            Debug.LogWarning("upgradeIndex is out of range: " + upgradeIndex);
        }
    }
}
