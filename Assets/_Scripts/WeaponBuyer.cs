using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBuyer : MonoBehaviour
{
    public GameObject weaponPrefab;
    public int weaponCost = 50;
    public Transform spawnPoint;
    public TMP_Text moneyText;

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
           
            scoreManager.AddPoints(-weaponCost);

            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);

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
