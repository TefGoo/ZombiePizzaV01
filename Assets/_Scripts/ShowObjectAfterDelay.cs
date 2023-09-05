using UnityEngine;
using System.Collections;

public class ShowObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToShow; // Drag the GameObject you want to show in the Inspector

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
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Activate the GameObject after the delay
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
