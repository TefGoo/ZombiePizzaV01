using UnityEngine;

public class BulletLifetime : MonoBehaviour
{
    public float lifetime = 8f; // Adjust this value to set the bullet's lifetime

    private void Start()
    {
        // Schedule the bullet to be destroyed after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}
