using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy
    public Transform[] spawnPoints; // Array of spawn points for the enemies
    public Transform destination; // Destination for the enemies to walk towards

    public float spawnInterval = 10f; // Spawn interval

    private float nextSpawnTime; // Time when the next enemy should spawn

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // Set the initial spawn time
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval; // Update the next spawn time
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is not assigned.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned for enemies.");
            return;
        }

        Transform spawnPoint = GetRandomSpawnPoint();
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Activate ZombieAI component
        ZombieAI zombieAI = enemy.GetComponentInChildren<ZombieAI>();
        if (zombieAI != null)
        {
            zombieAI.enabled = true;
            zombieAI.destination = destination;
        }
        else
        {
            Debug.LogWarning("Enemy prefab does not have a ZombieAI component.");
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
}
