using UnityEngine;
using UnityEngine.UI; // Import the UI namespace
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public int maxHealth = 20;
    public float damageCheckInterval = 1f;
    public float damageCooldown = 2f;
    public int damageAmount = 5;
    public Image healthBarImage; // Reference to the health bar image UI element

    private int currentHealth;
    private float nextDamageCheckTime;
    private float nextDamageTime;
    private bool isBeingAttacked;
    private int zombiesTouching;

    private void Start()
    {
        currentHealth = maxHealth;
        nextDamageCheckTime = Time.time + damageCheckInterval;
        nextDamageTime = Time.time;
        UpdateHealthBar(); // Call the method to update the health bar
    }

    private void Update()
    {
        if (isBeingAttacked && Time.time >= nextDamageCheckTime)
        {
            if (Time.time >= nextDamageTime)
            {
                DealDamage(damageAmount);
                nextDamageTime = Time.time + damageCooldown;
            }
            nextDamageCheckTime = Time.time + damageCheckInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesTouching++;
            isBeingAttacked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesTouching--;
            if (zombiesTouching <= 0)
            {
                zombiesTouching = 0;
                isBeingAttacked = false;
            }
        }
    }

    private void DealDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            DestroyStore();
        }

        UpdateHealthBar(); // Call the method to update the health bar
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

        UpdateHealthBar(); // Call the method to update the health bar
    }
}
