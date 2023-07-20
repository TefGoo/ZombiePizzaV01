using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    public int maxHealth = 10;
    public float damageCheckInterval = 3f;
    public TextMeshProUGUI healthText;

    private int currentHealth;
    private float nextDamageCheckTime;
    private int zombiesTouching;

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
            DealDamage(zombiesTouching);
            nextDamageCheckTime = Time.time + damageCheckInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesTouching++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesTouching--;
            if (zombiesTouching < 0)
            {
                zombiesTouching = 0;
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthText();
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
        Destroy(gameObject);
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
