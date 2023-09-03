using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Store : MonoBehaviour
{
    public int maxHealth = 20;
    public int damageAmount = 5;
    public Image healthBarImage;
    public Image storeIconImage; // Reference to the original store icon image
    public Image damagedStoreIcon; // Reference to the damaged store icon image

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Hide the damaged store icon at the start
        damagedStoreIcon.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            DealDamage(damageAmount);
        }
    }

    public void DealDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            DestroyStore();
        }

        UpdateHealthBar();

        // Hide the original store icon
        storeIconImage.gameObject.SetActive(false);

        // Show the damaged store icon
        damagedStoreIcon.gameObject.SetActive(true);

        // Start a coroutine to shake the damaged store icon
        StartCoroutine(ShakeIcon(damagedStoreIcon));

        // Start a coroutine to return to the original icon after a delay
        StartCoroutine(ReturnToOriginalIcon());
    }

    private IEnumerator ShakeIcon(Image icon)
    {
        Vector3 originalPosition = icon.rectTransform.localPosition;
        float shakeDuration = 0.2f;
        float shakeMagnitude = 5f;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            icon.rectTransform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the icon's position
        icon.rectTransform.localPosition = originalPosition;
    }

    private IEnumerator ReturnToOriginalIcon()
    {
        yield return new WaitForSeconds(0.8f); // Adjust the delay time as needed

        // Hide the damaged store icon
        damagedStoreIcon.gameObject.SetActive(false);

        // Show the original store icon
        storeIconImage.gameObject.SetActive(true);
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
