using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportDestination; // The destination where you want to teleport the player
    public Canvas teleportCanvas; // Reference to the Canvas component you want to show

    private void Start()
    {
        // Hide the teleportation canvas at the start
        teleportCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PerformTeleportation(other.transform));
        }
    }

    private IEnumerator PerformTeleportation(Transform playerTransform)
    {
        // Show the teleportation canvas
        teleportCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(.8f);


        // Teleport the player to the specified destination
        playerTransform.position = teleportDestination.position;

        yield return new WaitForSeconds(2f);

        // Hide the teleportation canvas
        teleportCanvas.gameObject.SetActive(false);
    }
}
