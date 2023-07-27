using UnityEngine;
using TMPro;
using System.Collections;

public class PizzaPickup : MonoBehaviour
{
    public GameObject pizza;
    public Transform pizzaParent;
    public Player player;
    public TextMeshProUGUI messageText; // Reference to the TMP UI Text
    private bool isEquipped = false;

    void Start()
    {
        pizza.GetComponent<Rigidbody>().isKinematic = true;
        // Hide the TMP UI Text at start
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isEquipped)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("ControllerDrop"))
            {
                Drop();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isEquipped)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ControllerEquip"))
            {
                if (!player.HasBox())
                {
                    Equip();
                }
                else
                {
                    ShowMessage("You already have an item");
                }
            }
        }
    }

    void Drop()
    {
        pizza.transform.parent = null;
        pizza.GetComponent<Rigidbody>().isKinematic = false;
        pizza.GetComponent<BoxCollider>().enabled = true;
        isEquipped = false;
        player.SetHasBox(false);
    }

    void Equip()
    {
        pizza.transform.position = pizzaParent.position;
        pizza.transform.rotation = pizzaParent.rotation;
        pizza.transform.SetParent(pizzaParent);
        pizza.GetComponent<Rigidbody>().isKinematic = true;
        pizza.GetComponent<BoxCollider>().enabled = false;
        isEquipped = true;
        player.SetHasBox(true);
    }

    void ShowMessage(string text)
    {
        messageText.text = text;
        messageText.gameObject.SetActive(true); // Show the TMP UI Text
        // Start a coroutine to hide the message after a short delay
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);
        messageText.text = ""; // Clear the text
        messageText.gameObject.SetActive(false); // Hide the TMP UI Text
    }
}
