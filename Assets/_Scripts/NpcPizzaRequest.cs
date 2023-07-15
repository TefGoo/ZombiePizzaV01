using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;  // Array of NPC GameObjects
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText;
    public string noOrdersText = "No pending orders";

    private float minRequestTime = 30f;  // Minimum time between requests (in seconds)
    private float maxRequestTime = 60f;  // Maximum time between requests (in seconds)
    private float nextRequestTime;  // Time when the next request will occur

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
        // Check if there are any active pizza requests
        bool hasActiveRequests = false;
        foreach (GameObject npcObject in npcObjects)
        {
            if (IsPizzaRequestActive(npcObject))
            {
                hasActiveRequests = true;
                break;
            }
        }

        // Activate or deactivate the pizza request object based on active requests
        if (!hasActiveRequests)
        {
            ActivatePizzaRequest(pizzaRequestObject, noOrdersText);
        }
        else
        {
            DeactivatePizzaRequest(pizzaRequestObject);
        }

        // Check if it's time for the next request
        if (Time.time >= nextRequestTime)
        {
            // Randomly select an NPC
            GameObject randomNpc = GetRandomNpc();

            if (randomNpc != null)
            {
                // Call the method to handle the pizza request for the selected NPC
                DeliverPizza(randomNpc);
            }

            // Calculate the time for the next request
            SetNextRequestTime();
        }
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
        ActivatePizzaRequest(npc, npc.name);
    }

    private void ActivatePizzaRequest(GameObject npcObject, string npcName)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.ActivatePizzaRequest(npcName);
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

    private bool IsPizzaRequestActive(GameObject npcObject)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        return pizzaRequestComponent != null && pizzaRequestComponent.IsActive();
    }

    private void SetNextRequestTime()
    {
        nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
    }
}
