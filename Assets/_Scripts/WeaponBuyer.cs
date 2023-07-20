using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBuyer : MonoBehaviour
{
    public GameObject weaponPrefab;
    public int weaponCost = 50;
    public Transform spawnPoint;
    public TMP_Text moneyText; // Reference to the UI Text element that displays the money

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        UpdateMoneyText();
    }

    public void BuyWeapon()
    {
        if (scoreManager.Score >= weaponCost)
        {
            // Deduct the cost from the player's money
            scoreManager.AddPoints(-weaponCost);

            // Spawn the weapon at the spawn point position
            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);

            // Update the money text after the purchase
            UpdateMoneyText();

            Debug.Log("Weapon bought and spawned!");
        }
        else
        {
            Debug.Log("Not enough money to buy the weapon!");
        }
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + scoreManager.Score.ToString();
        }
    }
}
