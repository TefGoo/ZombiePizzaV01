using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;  // Array of NPC GameObjects
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza

    private float minRequestTime = 30f;  // Minimum time between requests (in seconds)
    private float maxRequestTime = 60f;  // Maximum time between requests (in seconds)
    private float nextRequestTime;  // Time when the next request will occur
    public TextMeshProUGUI requestText;
    public string noOrdersText = "No pending orders";



    private void Start()
    {
        // Set the initial request time
        nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
    }

    private void Update()
    {
        // Check if there are any active pizza requests
        bool hasActiveRequests = false;
        foreach (GameObject npc in npcObjects)
        {
            NpcPizzaRequestObject pizzaRequestComponent = npc.GetComponent<NpcPizzaRequestObject>();
            if (pizzaRequestComponent != null && pizzaRequestComponent.gameObject.activeSelf)
            {
                hasActiveRequests = true;
                break;
            }
        }

        // If there are no active requests, display "No pending orders" on the canvas
        if (!hasActiveRequests)
        {
            NpcPizzaRequestObject pizzaRequestComponent = pizzaRequestObject.GetComponent<NpcPizzaRequestObject>();
            if (pizzaRequestComponent != null)
            {
                pizzaRequestComponent.ActivatePizzaRequest(noOrdersText);
            }
        }
        else
        {
            // If there are active requests, deactivate the pizza request object for the NPC
            NpcPizzaRequestObject pizzaRequestComponent = pizzaRequestObject.GetComponent<NpcPizzaRequestObject>();
            if (pizzaRequestComponent != null)
            {
                pizzaRequestComponent.DeactivatePizzaRequest();
            }
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
            nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
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
        // Activate the pizza request object for the NPC
        NpcPizzaRequestObject pizzaRequestComponent = npc.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            // Pass the NPC information to the ActivatePizzaRequest method
            string npcInfo = npc.name; // Change this line to get the NPC information you want to display
            pizzaRequestComponent.ActivatePizzaRequest(npcInfo);
        }
    }


}
