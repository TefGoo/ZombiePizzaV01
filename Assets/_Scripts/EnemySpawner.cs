using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy
    public Transform[] spawnPoints; // Array of spawn points for the enemies
    public Transform destination; // Destination for the enemies to walk towards

    public float minSpawnInterval = 10f; // Minimum spawn interval
    public float maxSpawnInterval = 15f; // Maximum spawn interval

    private float nextSpawnTime; // Time when the next enemy should spawn

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            SetNextSpawnTime();
        }
    }

    private void SpawnEnemy()
    {
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

    private void SetNextSpawnTime()
    {
        float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnTime = Time.time + randomInterval;
    }
}
