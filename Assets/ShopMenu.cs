using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject shopPanel;

    public BunnyPlacement bunnyPlacement;
    public GameObject bunnyPrefab;
    public int bunnyCost = 50;

    private void Start()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

    public void ToggleShop()
    {
        if (shopPanel == null)
        {
            Debug.LogError("Shop Panel is not assigned.");
            return;
        }

        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void SelectBunny()
    {
        if (!CanUseShop())
        {
            return;
        }

        bunnyPlacement.SelectBunny(bunnyPrefab, bunnyCost);

        Debug.Log("Bunny selected for placement.");
    }

    public void UpgradeDamage()
    {
        if (!CanUseShop())
        {
            return;
        }

        GameManager.Instance.UpgradeBunnyDamage();
    }

    public void UpgradeFireRate()
    {
        if (!CanUseShop())
        {
            return;
        }

        GameManager.Instance.UpgradeBunnyFireRate();
    }

    private bool CanUseShop()
    {
        return GameManager.Instance != null && GameManager.Instance.GameStarted && GameManager.Instance.IsBuildPhase;
    }
}