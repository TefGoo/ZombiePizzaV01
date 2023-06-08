using UnityEngine;
using TMPro;

public class NpcPizzaRequestObject : MonoBehaviour
{
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza
    public TextMeshProUGUI requestText; // Reference to the TMP Text component for NPC name

    public void ActivatePizzaRequest(string npcName)
    {
        requestText.text = npcName;
        pizzaRequestObject.SetActive(true);
    }

    public void DeactivatePizzaRequest()
    {
        requestText.text = "No pending orders";
        pizzaRequestObject.SetActive(false);
    }
}
