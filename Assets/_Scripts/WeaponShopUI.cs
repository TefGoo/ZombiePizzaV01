using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShopUI : MonoBehaviour
{
    public GameObject buyButtonPrompt;
    public GameObject buyButton;
    public GameObject weaponShop;
    public TMP_Text priceText;

    private WeaponShop weaponShopScript;

    private void Start()
    {
        weaponShopScript = weaponShop.GetComponent<WeaponShop>();
        UpdatePriceText();
        HideBuyButtonPrompt();
    }

    private void Update()
    {
        // Check if the player is near the weapon shop
        if (Vector3.Distance(transform.position, weaponShop.transform.position) <= 3f)
        {
            ShowBuyButtonPrompt();

            // Handle the player buying the weapon
            if (Input.GetKeyDown(KeyCode.E) && !weaponShopScript.IsWeaponBought())
            {
                BuyWeapon();
            }
        }
        else
        {
            HideBuyButtonPrompt();
        }
    }

    private void BuyWeapon()
    {
        weaponShopScript.BuyWeapon();
    }

    private void ShowBuyButtonPrompt()
    {
        buyButtonPrompt.SetActive(true);
        buyButton.SetActive(true);
    }

    private void HideBuyButtonPrompt()
    {
        buyButtonPrompt.SetActive(false);
        buyButton.SetActive(false);
    }

    private void UpdatePriceText()
    {
        priceText.text = "Price: $" + weaponShopScript.GetWeaponCost().ToString();
    }
}
