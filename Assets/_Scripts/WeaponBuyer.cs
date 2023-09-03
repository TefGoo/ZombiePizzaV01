using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WeaponBuyer : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject objectToHide; // Add reference to the object you want to hide
    public int weaponCost = 50;
    public Transform spawnPoint;
    public TMP_Text moneyText;
    public Transform objectToMove;
    public float moveDistance = 0.8f;
    public AudioSource audioSource;
    public TMP_Text notificationText; // Add reference to the TMP_Text for displaying notifications

    private ScoreManager scoreManager;
    private Vector3 originalObjectPosition;
    private bool isMoving = false;
    private bool isMovingObject = false;
    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        UpdateMoneyText();
        originalObjectPosition = objectToMove.position;

        if (audioSource == null)
        {
            // Attempt to find the AudioSource on the object
            audioSource = GetComponent<AudioSource>();
        }

        if (notificationText != null)
        {
            notificationText.text = ""; // Hide the notification text at the start
        }
    }
    public void BuyWeaponAndPerformActions()
    {
        if (scoreManager.Score >= weaponCost && !isMovingObject)
        {
            scoreManager.AddPoints(-weaponCost);
            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);

            if (objectToHide != null)
            {
                objectToHide.SetActive(false); // Hide the object
            }

            if (audioSource != null)
            {
                audioSource.Play(); // Play the audio
            }

            // Move the object
            MoveObjectSmoothly();

            // Update the money text UI
            UpdateMoneyText();
        }
        else
        {
            ShowNotification("Not enough money"); // Show the notification text
        }
    }

    private void ShowNotification(string message)
    {
        if (notificationText != null)
        {
            notificationText.text = message;
            StartCoroutine(HideNotificationAfterDelay());
        }
    }

    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay time as needed
        if (notificationText != null)
        {
            notificationText.text = ""; // Clear the notification text
        }
    }
    private void MoveObjectSmoothly()
    {
        isMovingObject = true;

        Vector3 targetPosition = objectToMove.position + new Vector3(0f, moveDistance, 0f);
        StartCoroutine(MoveObjectCoroutine(targetPosition));
    }

    private IEnumerator MoveObjectCoroutine(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 1.0f;
        Vector3 originalPosition = objectToMove.position;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5.0f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(objectToMove.position, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMovingObject = false;
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + scoreManager.Score.ToString();
        }
    }

}
