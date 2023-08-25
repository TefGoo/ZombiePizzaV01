using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;  // Array of NPC GameObjects
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private float minRequestTime = 41f;  // Minimum time between requests (in seconds)
    private float maxRequestTime = 50f;  // Maximum time between requests (in seconds)
    private float nextRequestTime;  // Time when the next request will occur

    private NpcPizzaRequestObject currentPizzaRequest;

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
                // The timer has ended, handle the request
                HandleRequestCompletion();
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
        string npcName = npc.name; // Get the name of the NPC
        string message = "Pizza for " + npcName; // Construct the message
        ActivatePizzaRequest(npc, message);
    }

    private void ActivatePizzaRequest(GameObject npcObject, string message)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.ActivatePizzaRequest(message);
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
    private void HandleRequestCompletion()
    {
        if (currentPizzaRequest != null)
        {
            // Replace 'HasDelivered' with the actual method call from your NpcPizzaRequestObject script
            bool deliveredOnTime = currentPizzaRequest.HasDeliveredOnTime();

            if (deliveredOnTime)
            {
                // Player delivered on time
                int pointValue = 10; // Or whatever point value you want to give
                ScoreManager.Instance.AddPoints(pointValue);

                // Display the updated score in the TMP Text component
                if (scoreText != null)
                {
                    scoreText.text = "$" + ScoreManager.Instance.Score;
                }

                // Deactivate the pizza request object of the NPC
                GameObject houseObject = currentPizzaRequest.gameObject;
                NpcPizzaRequestObject pizzaRequestComponent = houseObject.GetComponentInChildren<NpcPizzaRequestObject>();
                if (pizzaRequestComponent != null)
                {
                    pizzaRequestComponent.DeactivatePizzaRequest();
                    pizzaRequestComponent.DeactivateHouse(); // Deactivate the house GameObject
                }
            }
            else
            {
                // Player didn't deliver on time, cancel the request
                CancelPizzaRequest();
                DeactivatePizzaRequest(currentPizzaRequest.gameObject);
            }
        }
    }
}
