using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Reference to the panel GameObject
    private bool panelActive; // Flag to track the panel's active state

    private void Update()
    {
        // Check for Tab key press on the keyboard or Options button press on the controller
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Options"))
        {
            panelActive = !panelActive; // Toggle the panel's active state

            // Activate or deactivate the panel based on the active state
            panel.SetActive(panelActive);
        }
    }
}
