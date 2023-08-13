using UnityEngine;
using TMPro;

public class PointBox : MonoBehaviour
{
    public int correctFlavorPoints = 10;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;

    private string requestedFlavor; // Store the requested flavor

    public void SetRequestedFlavor(string flavor)
    {
        requestedFlavor = flavor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            Debug.Log("Delivered Pizza Tag: " + gameObject.tag);
            GameObject houseObject = other.gameObject;
            NpcPizzaRequestObject pizzaRequestComponent = houseObject.GetComponentInChildren<NpcPizzaRequestObject>();
            if (pizzaRequestComponent != null && pizzaRequestComponent.IsActive())
            {
                if (requestedFlavor == gameObject.tag)
                {
                    ScoreManager.Instance.AddPoints(correctFlavorPoints);
                    feedbackText.text = "Correct flavor delivered!";
                    pizzaRequestComponent.DeactivateHouse(); // Hide the house object
                }
                else
                {
                    feedbackText.text = "Wrong flavor delivered!";
                }

                feedbackText.gameObject.SetActive(true);
                scoreText.text = "$ " + ScoreManager.Instance.Score;

                pizzaRequestComponent.DeactivatePizzaRequest();

                Destroy(gameObject);
            }
        }
    }
}
