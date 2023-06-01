using UnityEngine;

public class NpcPizzaRequestObject : MonoBehaviour
{
    public GameObject pizzaRequestObject; // The object to activate when an NPC requests a pizza

    public void ActivatePizzaRequest()
    {
        pizzaRequestObject.SetActive(true);
    }

    public void DeactivatePizzaRequest()
    {
        pizzaRequestObject.SetActive(false);
    }
}
