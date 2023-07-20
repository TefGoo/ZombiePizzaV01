using UnityEngine;
using TMPro;

public class HealthStore : MonoBehaviour
{
    public int healingPointsCost = 5;
    public int healingPointsValue = 1;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    private ScoreManager scoreManager;
    private Store store;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        store = GetComponent<Store>();
        UpdateMoneyText();
        UpdateHealthText();
    }

    public void BuyHealingPoints()
    {
        int totalCost = healingPointsCost * healingPointsValue;

        if (scoreManager.Score >= totalCost)
        {
            scoreManager.AddPoints(-totalCost);

            if (store != null)
            {
                store.Heal(healingPointsValue);
                UpdateHealthText();
            }
            else
            {
                Debug.LogError("Store reference is missing or not assigned!");
            }

            UpdateMoneyText();
        }
        else
        {
            Debug.Log("Not enough money to buy healing points!");
        }
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null && scoreManager != null)
        {
            moneyText.text = "$ " + scoreManager.Score;
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null && store != null)
        {
            healthText.text = "Health: " + store.GetCurrentHealth().ToString();
        }
    }
}
