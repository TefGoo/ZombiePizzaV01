using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;
    public GameObject pizzaRequestObject;
    public GameObject timerUI;
    public TextMeshProUGUI requestText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI feedbackText; // New TMP text for feedback messages

    private float minRequestTime = 30f;
    private float maxRequestTime = 60f;
    private float nextRequestTime;

    private NpcPizzaRequestObject currentPizzaRequest;
    private string[] pizzaFlavors = { "Pepperoni", "Margherita", "Supreme", "Hawaiian", "Vegetarian" };

    private void Start()
    {
        foreach (GameObject npcObject in npcObjects)
        {
            DeactivatePizzaRequest(npcObject);
        }

        SetNextRequestTime();
        feedbackText.gameObject.SetActive(false); // Hide the feedback text initially
    }

    private void Update()
    {
        if (Time.time >= nextRequestTime)
        {
            GameObject randomNpc = GetRandomNpc();

            if (randomNpc != null)
            {
                currentPizzaRequest = randomNpc.GetComponent<NpcPizzaRequestObject>();
                if (currentPizzaRequest != null)
                {
                    DeliverPizza(randomNpc);
                    SetTimer();
                }
            }

            SetNextRequestTime();
        }

        if (currentPizzaRequest != null)
        {
            if (currentPizzaRequest.IsActive() && Time.time >= currentPizzaRequest.TimerEndTime)
            {
                CancelPizzaRequest();
            }
            else
            {
                timerText.text = Mathf.CeilToInt(currentPizzaRequest.TimerEndTime - Time.time).ToString();
            }
        }

        if (currentPizzaRequest == null || !currentPizzaRequest.IsActive())
        {
            ActivatePizzaRequest(pizzaRequestObject, "No pending orders");
            timerUI.SetActive(false);
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
        timerUI.SetActive(true);
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

        Debug.Log("Requested Flavor Tag: " + requestedFlavor);
    }

    private void ActivatePizzaRequest(GameObject npcObject, string flavor)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.ActivatePizzaRequest(flavor);
            requestText.text = "Order: " + flavor + " pizza for " + npcObject.name;
        }
    }

    private void DeactivatePizzaRequest(GameObject npcObject)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.DeactivatePizzaRequest();
            requestText.text = "No pending orders";
        }
    }

    private void CancelPizzaRequest()
    {
        if (currentPizzaRequest != null)
        {
            currentPizzaRequest.CancelRequest();
            currentPizzaRequest = null;

            timerUI.SetActive(false);
        }
    }
}
