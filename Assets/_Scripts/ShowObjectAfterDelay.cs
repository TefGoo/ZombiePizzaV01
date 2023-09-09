using UnityEngine;
using System.Collections;

public class ShowObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToShow; // Drag the GameObject you want to show in the Inspector
    public float delay = 3f;       // The delay in seconds before showing the object

    private void Start()
    {
        // Disable the GameObject at the start of the scene
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }

        // Call a method to show the GameObject after a delay
        StartCoroutine(ShowObjectDelayed());
    }

    private IEnumerator ShowObjectDelayed()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Activate the GameObject after the delay
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
