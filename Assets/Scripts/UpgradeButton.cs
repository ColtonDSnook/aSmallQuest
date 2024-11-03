using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int upgradeIndex; // The index of the upgrade this button applies
    public UpgradeManager upgradeManager;

    public bool isDisabled = true;
    public UpgradeButton previousUpgrade;
    public GameObject checkMark;

    public void OnButtonClick()
    {
        if (upgradeManager.upgrades[upgradeIndex].isPurchased)
        {
            Debug.Log("Cannot Repurchase");
        }
        else if (isDisabled)
        {
            Debug.Log("Purchase previous upgrades to unlock this upgrade");
        }
        else if (!isDisabled)
        {
            upgradeManager.ShowUpgradeDescription(upgradeIndex);
            Debug.Log("Clicked");
        }
    }

    private void Update()
    {
        if (previousUpgrade == null)
        {
            isDisabled = false;
        }
        else if (upgradeManager.upgrades[previousUpgrade.upgradeIndex].isPurchased)
        {
            isDisabled = false;
        }
        else
        {
            isDisabled = true;
        }


        // Ensure the upgradeIndex is within the bounds of the upgrades list
        if (upgradeIndex >= 0 && upgradeIndex < upgradeManager.upgrades.Count)
        {
            if (!upgradeManager.upgrades[upgradeIndex].isPurchased)
            {
                checkMark.SetActive(false);
            }
            // Check if the upgrade is purchased
            if (upgradeManager.upgrades[upgradeIndex].isPurchased)
            {
                GetComponent<Image>().color = Color.gray;
                checkMark.SetActive(true);
            }
            else if (isDisabled)
            {
                GetComponent<Image>().color = Color.gray;
            }
            else if (!isDisabled)
            {
                GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            Debug.LogWarning("upgradeIndex is out of range: " + upgradeIndex);
        }
    }
}
