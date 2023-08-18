using TMPro;
using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcObjects;
    public GameObject pizzaRequestObject;
    public GameObject timerUI;
    public TextMeshProUGUI requestText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI feedbackText;

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
        feedbackText.gameObject.SetActive(false);
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
                    currentPizzaRequest.RequestPizza(randomNpc.name); // Request pizza flavor for the NPC
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
            ActivatePizzaRequest(pizzaRequestObject, "No pending orders", "");

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

    private void ActivatePizzaRequest(GameObject npcObject, string flavor, string npcName)
    {
        NpcPizzaRequestObject pizzaRequestComponent = npcObject.GetComponent<NpcPizzaRequestObject>();
        if (pizzaRequestComponent != null)
        {
            pizzaRequestComponent.ActivatePizzaRequest(flavor, npcName);
            requestText.text = "Order: " + flavor + " pizza for " + npcName;
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
