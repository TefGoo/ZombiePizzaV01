using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public BoxCollider mainCollider;
    public GameObject ZombieRig;
    public Animator ZombieAnimator;
    [SerializeField]
    private MonoBehaviour enemyAI; // Serialized field for the enemy AI script

    private void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RDHit")
        {
            RagdollModeOn();
            DeactivateEnemyAI(); // Deactivate ZombieAI script
            StartCoroutine(DestroyZombieAfterDelay());
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
        foreach (Collider collider in ZombieColliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigid in ZombieRigidbodies)
        {
            rigid.isKinematic = true;
        }

        ZombieAnimator.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void DeactivateEnemyAI()
    {
        if (enemyAI != null)
        {
            enemyAI.enabled = false; // Disable the specific enemy AI script
        }
    }

    IEnumerator DestroyZombieAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Destroy the zombie object
        Destroy(gameObject);
    }
}