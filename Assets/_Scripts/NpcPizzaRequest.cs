using UnityEngine;

public class NpcPizzaRequest : MonoBehaviour
{
    public GameObject[] npcs;  // Array of NPC game objects

    private float minRequestTime = 30f;  // Minimum time between requests (in seconds)
    private float maxRequestTime = 60f;  // Maximum time between requests (in seconds)
    private float nextRequestTime;  // Time when the next request will occur

    private void Start()
    {
        // Set the initial request time
        nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
    }

    private void Update()
    {
        // Check if it's time for the next request
        if (Time.time >= nextRequestTime)
        {
            // Randomly select an NPC
            int randomNpcIndex = Random.Range(0, npcs.Length);
            GameObject randomNpc = npcs[randomNpcIndex];

            // Call the method to handle the pizza request for the selected NPC
            DeliverPizza(randomNpc);

            // Calculate the time for the next request
            nextRequestTime = Time.time + Random.Range(minRequestTime, maxRequestTime);
        }
    }

    private void DeliverPizza(GameObject npc)
    {
        // Here you can add your code to deliver the pizza to the NPC.
        // You can access the NPC's specific properties or trigger
        // any necessary actions based on the NPC that requested the pizza.
        Debug.Log("Pizza delivered to NPC: " + npc.name);
    }
}
