using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    private TextMeshProUGUI ammoText;

    private int remainingShots = 5; // The initial number of remaining shots

    private void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
        UpdateAmmoText();
    }

    public void UpdateAmmo(int newRemainingShots)
    {
        remainingShots = newRemainingShots;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + remainingShots.ToString();
    }
}
