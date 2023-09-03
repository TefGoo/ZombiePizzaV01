using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthStore : MonoBehaviour
{
    public int healingPointsCost = 5;
    public int healingPointsValue = 1;
    public TextMeshProUGUI moneyText;
    public Image healthBar; // Reference to the health bar's foreground image

    private ScoreManager scoreManager;
    private Store store;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        store = GetComponent<Store>();
        UpdateMoneyText();
        UpdateHealthBar();
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
                UpdateHealthBar();
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

    public void TakeDamage(int damageAmount)
    {
        // Update the building's health (assuming you have a function for this)
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null && scoreManager != null)
        {
            moneyText.text = "$ " + scoreManager.Score;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null && store != null)
        {
            float fillAmount = (float)store.GetCurrentHealth() / store.maxHealth;
            healthBar.fillAmount = fillAmount;
        }
    }
}
