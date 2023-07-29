using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;  // Array of NPC GameObjects
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText;
    public TextMeshProUGUI timerText;

    private float minRequestTime = 30f;  // Minimum time between requests (in seconds)
    private float maxRequestTime = 60f;  // Maximum time between requests (in seconds)
    private float nextRequestTime;  // Time when the next request will occur

    private NpcPizzaRequestObject currentPizzaRequest;
    private string[] pizzaFlavors = { "Pepperoni", "Margherita", "Supreme", "Hawaiian", "Vegetarian" };

    private void Start()
    {
        // Deactivate all house objects initially
        foreach (GameObject npcObject in npcObjects)
        {
            DeactivatePizzaRequest(npcObject);
        }

        // Set the initial request time
        SetNextRequestTime();
    }

    private void Update()
    {
        // Check if it's time for the next request
        if (Time.time >= nextRequestTime)
        {
            // Randomly select an NPC
            GameObject randomNpc = GetRandomNpc();

            if (randomNpc != null)
            {
                // Call the method to handle the pizza request for the selected NPC
                currentPizzaRequest = randomNpc.GetComponent<NpcPizzaRequestObject>();
                if (currentPizzaRequest != null)
                {
                    DeliverPizza(randomNpc);
                    SetTimer();
                }
            }

            // Calculate the time for the next request
            SetNextRequestTime();
        }

        // Check for the timer
        if (currentPizzaRequest != null)
        {
            if (currentPizzaRequest.IsActive() && Time.time >= currentPizzaRequest.TimerEndTime)
            {
                // The timer has ended, cancel the request
                CancelPizzaRequest();
            }
            else
            {
                // Update the timer text in the UI
                timerText.text = Mathf.CeilToInt(currentPizzaRequest.TimerEndTime - Time.time).ToString();
            }
        }

        // Activate or deactivate the pizza request object based on active requests
        if (currentPizzaRequest == null || !currentPizzaRequest.IsActive())
        {
            ActivatePizzaRequest(pizzaRequestObject, "No pending orders");
        }
        else
        {
            DeactivatePizzaRequest(pizzaRequestObject);
        }
    }

    private void SetNextRequestTime()
    {
        nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
    }

    private void SetTimer()
    {
        currentPizzaRequest.StartTimer();
    }

    private GameObject GetRandomNpc()
    {
        if (npcObjects.Length > 0)
        {
            int randomNpcIndex = Random.Range(0, npcObjects.Length);
            return npcObjects[randomNpcIndex];
        }

        return null;
    }

    private void DeliverPizza(GameObject npc)
    {
        int randomFlavorIndex = Random.Range(0, pizzaFlavors.Length);
        string requestedFlavor = pizzaFlavors[randomFlavorIndex];
        ActivatePizzaRequest(npc, requestedFlavor);
    }

    private void ActivatePizzaRequest(GameObject npcObject, string flavor)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.ActivatePizzaRequest(flavor);
        }
    }

    private void DeactivatePizzaRequest(GameObject npcObject)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.DeactivatePizzaRequest();
        }
    }

    private void CancelPizzaRequest()
    {
        if (currentPizzaRequest != null)
        {
            currentPizzaRequest.CancelRequest();
            currentPizzaRequest = null;
        }
    }
}
