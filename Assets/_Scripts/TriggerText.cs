using UnityEngine;
using TMPro;

public class TriggerText : MonoBehaviour
{
    public TextMeshProUGUI textToShow; // Reference to your TextMeshPro text element
    private bool playerInsideTrigger = false;

    private void Start()
    {
        textToShow.gameObject.SetActive(false); // Hide the text on start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // You can change "Player" to the tag of your player GameObject
        {
            playerInsideTrigger = true;
            textToShow.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            textToShow.gameObject.SetActive(false);
        }
    }

}
