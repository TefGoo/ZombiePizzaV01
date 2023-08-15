using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseButton : MonoBehaviour
{
    public WeaponBuyer weaponBuyer;
    public float activationDelay = 5.0f;
    private bool canActivate = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canActivate)
        {
            weaponBuyer.BuyWeaponAndPerformActions();

            canActivate = false;
            StartCoroutine(ResetActivation());
        }
    }

    private IEnumerator ResetActivation()
    {
        yield return new WaitForSeconds(activationDelay);
        canActivate = true;
    }
}
