using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public BoxCollider mainCollider;
    public GameObject ZombieRig;
    public Animator ZombieAnimator;

    private void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "RDHit")
        {
            RagdollModeOn();
        }

    }

    Collider[] ZombieColliders;
    Rigidbody[] ZombieRigidbodies;
    void GetRagdollBits()
    {
        ZombieColliders = ZombieRig.GetComponentsInChildren<Collider>();
        ZombieRigidbodies = ZombieRig.GetComponentsInChildren<Rigidbody>();
    }
    void RagdollModeOn()
    {
        ZombieAnimator.enabled = false;
        foreach (Collider collider in ZombieColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rigid in ZombieRigidbodies)
        {
            rigid.isKinematic = false;
        }
        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

    }

    void RagdollModeOff()
    {
        foreach(Collider collider in ZombieColliders)
        {
            collider.enabled = false;
        }

        foreach(Rigidbody rigid in  ZombieRigidbodies)
        {
            rigid.isKinematic = true;
        }

        ZombieAnimator.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

    }
    
}
