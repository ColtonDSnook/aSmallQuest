using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Upgrade;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int upgradeIndex; // The index of the upgrade this button applies
    public UpgradeManager upgradeManager;

    public bool isDisabled = true;
    public UpgradeButton previousUpgrade;
    public GameObject checkMark;
    public GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        checkMark = transform.GetChild(0).gameObject;
        //6AFF51 hexcode for purchasable upgrade
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("HOVERED OBJECT");
        upgradeManager.ShowUpgradeDescription(upgradeIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeManager.HideUpgradeDescription();
    }

    public void OnButtonClick()
    {
        if (upgradeManager.upgrades[upgradeIndex].isPurchased)
        {
            StartCoroutine(upgradeManager.ShowErrorMessage("Cannot Repurchase"));
            //Debug.Log("Cannot Repurchase");
        }
        else if (isDisabled)
        {
            StartCoroutine(upgradeManager.ShowErrorMessage("Purchase previous upgrades to unlock this upgrade"));
            //Debug.Log("Purchase previous upgrades to unlock this upgrade");
        }
        else if (!isDisabled)
        {
            upgradeManager.ConfirmUpgrade(upgradeIndex);
            //Debug.Log("Clicked");
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

            if (!upgradeManager.upgrades[upgradeIndex].isPurchased && !isDisabled)
            {
                if (gameManager.gold >= upgradeManager.upgrades[upgradeIndex].cost)
                {
                    //Debug.Log(gameManager.gold >= upgradeManager.upgrades[upgradeIndex].cost);
                    GetComponent<Image>().color = Color.green;
                    //GetComponent<Image>().color = new Color(106, 255, 81, 255);
                }
            }

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
            else if (!isDisabled && gameManager.gold < upgradeManager.upgrades[upgradeIndex].cost)
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
