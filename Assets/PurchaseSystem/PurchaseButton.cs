using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseButton : MonoBehaviour
{
    public UnityEvent Purchase;
    [SerializeField] private int _purchaseCost = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO optionally check if Player
            // check if player has enough munny'
            // get access to player's money
            // check if playerMoney > _purchaseCost
            // if so, subtract _purchase cost from player money
            // and purchase!
            Purchase?.Invoke();
        }
    }
}
