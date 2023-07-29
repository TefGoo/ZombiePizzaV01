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
    public float spriteDisplayTime = 1f;

    private int currentHealth;
    private bool isGameOver = false;
    private float timeToShowSprites;

    private void Start()
    {
        currentHealth = maxHealth;
        HideHealthSprites();
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
        else
        {
            // Show health sprites temporarily
            ShowHealthSprites();
        }
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

    private void ShowHealthSprites()
    {
        timeToShowSprites = Time.time + spriteDisplayTime;
        healthImage.enabled = true;
    }

    private void HideHealthSprites()
    {
        healthImage.enabled = false;
    }

    private void Update()
    {
        // Hide health sprites after the display time has passed
        if (Time.time >= timeToShowSprites)
        {
            HideHealthSprites();
        }
    }
}
