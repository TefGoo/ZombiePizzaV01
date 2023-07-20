using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public int maxHealth = 20;
    public float damageCheckInterval = 1f; // Reduce the interval for more responsive damage
    public TextMeshProUGUI healthText;

    private int currentHealth;
    private float nextDamageCheckTime;
    private bool isBeingAttacked; // Flag to check if the store is being attacked
    private int zombiesTouching; // Add this line to fix the error

    private void Start()
    {
        currentHealth = maxHealth;
        nextDamageCheckTime = Time.time + damageCheckInterval;
        UpdateHealthText();
    }

    private void Update()
    {
        if (Time.time >= nextDamageCheckTime)
        {
            if (isBeingAttacked)
            {
                DealDamage(zombiesTouching); // Deal damage based on the number of zombies touching
            }
            nextDamageCheckTime = Time.time + damageCheckInterval;
        }
    }
    public void Heal(int amount) // Rename 'DealDamage' to 'Heal'
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesTouching++;
            isBeingAttacked = true; // A zombie has entered the trigger, the store is being attacked
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
                isBeingAttacked = false; // No zombies are touching, the store is not being attacked
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

        UpdateHealthText();
    }

    private void DestroyStore()
    {
        // Perform any actions or effects when the store is destroyed
        Debug.Log("Store has been destroyed!");

        // Load a new scene, you can replace "YourSceneName" with the actual name of your scene
        SceneManager.LoadScene("StoreDestroyed");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
}
