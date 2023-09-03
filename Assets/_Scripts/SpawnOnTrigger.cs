using UnityEngine;
using TMPro;
using System.Collections;
public class SpawnOnTrigger : MonoBehaviour
{
    public string spawnButton;
    public GameObject prefab;
    public Transform spawnPoint;
    public TextMeshProUGUI countdownText; // Reference to your TMP text component

    private bool playerInside;
    private bool spawning;
    private float spawnDelay = 5f;
    private float currentSpawnDelay;

    private void Start()
    {
        countdownText.gameObject.SetActive(false); // Hide the text at the start of the game
    }

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
        if (playerInside && (Input.GetButtonDown(spawnButton) || Input.GetButtonDown("Spawn")) && !spawning)
        {
            StartCoroutine(SpawnWithDelay());
        }
    }

    private IEnumerator SpawnWithDelay()
    {
        spawning = true;
        currentSpawnDelay = spawnDelay;

        countdownText.text = "";
        countdownText.gameObject.SetActive(true);

        while (currentSpawnDelay > 0)
        {
            countdownText.text = currentSpawnDelay.ToString("0");
            yield return new WaitForSeconds(1f);
            currentSpawnDelay -= 1f;
        }

        countdownText.text = "Pizza is ready!";
        yield return new WaitForSeconds(1f);

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("New Pizza baked!");

        countdownText.text = "";
        countdownText.gameObject.SetActive(false);

        spawning = false;
    }
}
