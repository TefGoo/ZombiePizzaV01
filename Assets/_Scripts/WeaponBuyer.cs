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

    private ScoreManager scoreManager;
    private Vector3 originalObjectPosition;
    private bool isMoving = false;

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
    }

    public void BuyWeaponAndPerformActions()
    {
        if (scoreManager.Score >= weaponCost && !isMoving)
        {
            scoreManager.AddPoints(-weaponCost);
            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);

            StartCoroutine(MoveObjectSmoothly());

            if (objectToHide != null)
            {
                objectToHide.SetActive(false); // Hide the object
            }

            if (audioSource != null)
            {
                audioSource.Play(); // Play the audio
            }
        }
    }

    private IEnumerator MoveObjectSmoothly()
    {
        isMoving = true;

        Vector3 targetPosition = objectToMove.position + new Vector3(0f, moveDistance, 0f);
        float elapsedTime = 0f;
        float duration = 1.0f;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(objectToMove.position, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5.0f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(objectToMove.position, originalObjectPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + scoreManager.Score.ToString();
        }
    }
}
