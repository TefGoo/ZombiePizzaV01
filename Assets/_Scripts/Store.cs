using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public int maxHealth = 20;
    public int damageAmount = 5;
    public Image healthBarImage;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            DealDamage(damageAmount);
        }
    }

    private void DealDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            DestroyStore();
        }

        UpdateHealthBar();
    }

    private void DestroyStore()
    {
        Debug.Log("Store has been destroyed!");
        SceneManager.LoadScene("StoreDestroyed");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = fillAmount;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
    }
}
