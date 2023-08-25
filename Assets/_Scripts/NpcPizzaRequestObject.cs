using UnityEngine;
using TMPro;

public class NpcPizzaRequestObject : MonoBehaviour
{
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText; // Reference to the TMP Text component for NPC name
    public GameObject houseObject;

    public float TimerEndTime { get; private set; }
    private bool isActive;

    private bool hasDelivered; // Flag to track whether the pizza has been delivered

    public void ActivatePizzaRequest(string npcName)
    {
        requestText.text = npcName;
        pizzaRequestObject.SetActive(true);
        houseObject.SetActive(true);
        isActive = true;
        hasDelivered = false; // Reset the delivery status
    }

    public void DeactivatePizzaRequest()
    {
        requestText.text = "No pending orders";
        pizzaRequestObject.SetActive(false);
        houseObject.SetActive(false);
        isActive = false;
        hasDelivered = false; // Reset the delivery status
    }

    public void DeactivateHouse()
    {
        houseObject.SetActive(false);
    }

    public void StartTimer()
    {
        TimerEndTime = Time.time + 40f; // 40 seconds timer
    }

    public void CancelRequest()
    {
        DeactivatePizzaRequest();
    }

    public bool IsActive()
    {
        return isActive;
    }

    // Method to mark the pizza as delivered
    public void MarkDelivered()
    {
        hasDelivered = true;
    }

    // Method to check if the pizza has been delivered on time
    public bool HasDeliveredOnTime()
    {
        return isActive && hasDelivered && Time.time < TimerEndTime;
    }
}
