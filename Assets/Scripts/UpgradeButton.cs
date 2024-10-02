using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public int upgradeIndex; // The index of the upgrade this button applies
    public UpgradeManager upgradeManager;

    public void OnButtonClick()
    {
        upgradeManager.ApplyUpgrade(upgradeIndex);
    }
}
