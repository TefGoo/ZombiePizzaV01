using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public Image healthImage;
    public Sprite[] healthSprites;
    public string gameOverSceneName;
    public Animator playerAnimator;
    public float gameOverDelay = 2f;

    private int currentHealth;
    private bool isGameOver = false;


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver)
            return;

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Player has no health points left, trigger game over
            GameOver();
        }
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthUI()
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthImage.sprite = healthSprites[currentHealth];
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        GetComponent<Player>().enabled = false;
        playerAnimator.SetTrigger("DeathTrigger");
        Invoke("LoadGameOverScene", gameOverDelay);
    }

    private void LoadGameOverScene()
    {
        // Load the game over scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}
