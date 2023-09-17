using UnityEngine;
using TMPro;
using System.Collections;

public class PizzaPickup : MonoBehaviour
{
    public enum PickupType
    {
        Box,
        Gun
    }

    public PickupType pickupType; // Specify the pickup type in the Inspector
    public GameObject pizza;
    public Transform pizzaParent;
    public Player player;
    public TextMeshProUGUI messageText; // Reference to your message TMP Text
    public TextMeshProUGUI ammoText;    // Reference to your ammo TMP Text

    private bool isEquipped = false;

    void Start()
    {
        pizza.GetComponent<Rigidbody>().isKinematic = true;
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

        // Check if the player has the gun and update ammo text visibility accordingly
        if (player.HasGun())
        {
            ammoText.gameObject.SetActive(true);
        }
        else
        {
            ammoText.gameObject.SetActive(false);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isEquipped)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ControllerEquip")) && !player.HasBox() && !player.HasGun())
            {
                if (pickupType == PickupType.Box)
                {
                    EquipBox();
                }
                else if (pickupType == PickupType.Gun)
                {
                    EquipGun();
                }
            }
            else if (player.HasBox() || player.HasGun())
            {
                ShowMessage("You already have an item");
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
        player.SetHasGun(false);
    }

    void EquipBox()
    {
        pizza.transform.position = pizzaParent.position;
        pizza.transform.rotation = pizzaParent.rotation;
        pizza.transform.SetParent(pizzaParent);
        pizza.GetComponent<Rigidbody>().isKinematic = true;
        pizza.GetComponent<BoxCollider>().enabled = false;
        isEquipped = true;
        player.SetHasBox(true);
        player.SetHasGun(false);
        // Disable the "FloatingObject" script component
        FloatingObject floatingObject = pizza.GetComponent<FloatingObject>();
        if (floatingObject != null)
        {
            floatingObject.enabled = false;
        }

        isEquipped = true;
        player.SetHasBox(true);
        player.SetHasGun(false);
    }

    void EquipGun()
    {
        // Your gun equip logic here
        // Similar to EquipBox but for the gun
        pizza.transform.position = pizzaParent.position;
        pizza.transform.rotation = pizzaParent.rotation;
        pizza.transform.SetParent(pizzaParent);
        pizza.GetComponent<Rigidbody>().isKinematic = true;
        pizza.GetComponent<BoxCollider>().enabled = false;
        isEquipped = true;
        player.SetHasBox(false);
        player.SetHasGun(true);
        // Disable the "FloatingObject" script component
        FloatingObject floatingObject = pizza.GetComponent<FloatingObject>();
        if (floatingObject != null)
        {
            floatingObject.enabled = false;
        }

        isEquipped = true;
        player.SetHasBox(false);
        player.SetHasGun(true);
    }

    void ShowMessage(string text)
    {
        messageText.text = text;
        messageText.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }
}
