using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    public KeyCode spawnKey;
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
        if (playerInside && Input.GetKeyDown(spawnKey))
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("New Pizza baked!");
        }
    }
}
