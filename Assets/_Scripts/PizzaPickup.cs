using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPickup : MonoBehaviour
{
    public GameObject pizza;
    public Transform pizzaParent;

    private bool isEquipped = false;

    void Start()
    {
        pizza.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (isEquipped)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isEquipped)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Equip();
            }
        }
    }

    void Drop()
    {
        pizza.transform.parent = null;
        pizza.GetComponent<Rigidbody>().isKinematic = false;
        pizza.GetComponent<BoxCollider>().enabled = true;
        isEquipped = false;
    }

    void Equip()
    {
        pizza.transform.position = pizzaParent.position;
        pizza.transform.rotation = pizzaParent.rotation;
        pizza.transform.SetParent(pizzaParent);
        pizza.GetComponent<Rigidbody>().isKinematic = true;
        pizza.GetComponent<BoxCollider>().enabled = false;
        isEquipped = true;
    }
}
