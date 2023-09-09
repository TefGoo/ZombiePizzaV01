using UnityEngine;

public class DeactivateObjectAfterDelay : MonoBehaviour
{
    public float delay = 12f; // The delay in seconds before deactivating the object.

    private void Start()
    {
        // Invoke the DeactivateObject method with the specified delay.
        Invoke("DeactivateObject", delay);
    }

    private void DeactivateObject()
    {
        // Deactivate the GameObject when the delay is reached.
        gameObject.SetActive(false);
    }
}
