using UnityEngine;
using TMPro;

public class PointBox : MonoBehaviour
{
    public int pointValue = 10;   // The number of points to give to the player
    public TextMeshProUGUI scoreText;   // Reference to the TMP Text component where the score will be displayed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            // Give points to the player
            ScoreManager.Instance.AddPoints(pointValue);

            // Display the updated score in the TMP Text component
            scoreText.text = "$ " + ScoreManager.Instance.Score;

            // Destroy the box object
            Destroy(gameObject);
        }
    }
}
