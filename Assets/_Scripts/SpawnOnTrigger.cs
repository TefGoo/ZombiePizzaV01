using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    public string spawnButton;
    public GameObject prefab;
    public Transform spawnPoint;

    private bool playerInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && (Input.GetButtonDown(spawnButton) || Input.GetButtonDown("Spawn")))
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("New Pizza baked!");
        }
    }
}