using UnityEngine;
using TMPro;

public class NpcPizzaRequestObject : MonoBehaviour
{
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText; // Reference to the TMP Text component for NPC name
    public GameObject houseObject;

    private string requestedFlavor; // New variable to store the requested pizza flavor
    private float timerEndTime;
    private bool isActive;

    public string RequestedFlavor
    {
        get { return requestedFlavor; }
    }

    public float TimerEndTime
    {
        get { return timerEndTime; }
    }

    public void RequestPizza(string npcName)
    {
        requestedFlavor = GetRandomFlavor(); // Get a random pizza flavor
        ActivatePizzaRequest(npcName, requestedFlavor);
        StartTimer();
    }

    private string GetRandomFlavor()
    {
        int randomFlavorIndex = Random.Range(0, pizzaFlavors.Length);
        return pizzaFlavors[randomFlavorIndex];
    }

    public void ActivatePizzaRequest(string npcName, string flavor)
    {
        requestText.text = "Order: " + flavor + " pizza for " + npcName;
        pizzaRequestObject.SetActive(true);
        houseObject.SetActive(true);
        isActive = true;
    }

    public void DeactivatePizzaRequest()
    {
        requestText.text = "No pending orders";
        pizzaRequestObject.SetActive(false);
        houseObject.SetActive(false);
        isActive = false;
    }

    public void DeactivateHouse()
    {
        houseObject.SetActive(false);
    }

    public void StartTimer()
    {
        timerEndTime = Time.time + GetRandomRequestTime(); // Set timer end time based on your logic
    }

    private float GetRandomRequestTime()
    {
        return Random.Range(minRequestTime, maxRequestTime);
    }

    public void CancelRequest()
    {
        DeactivatePizzaRequest();
    }

    public bool IsActive()
    {
        return isActive;
    }

    private string[] pizzaFlavors = { "Pepperoni", "Margherita", "Supreme", "Hawaiian", "Vegetarian" };
    private float minRequestTime = 30f;
    private float maxRequestTime = 60f;
}
