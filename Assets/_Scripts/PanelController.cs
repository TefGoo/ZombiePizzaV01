using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Reference to the panel GameObject
    public GameObject objectToHide; // Reference to the GameObject you want to hide/show
    private bool panelActive; // Flag to track the panel's active state


    private void Start()
    {
        // Deactivate the panel GameObject at the start
        panel.SetActive(false);
        panelActive = false; // Make sure the panelActive flag matches the initial state
    }
    private void Update()
    {
        // Check for Tab key press on the keyboard or Options button press on the controller
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Options"))
        {
            panelActive = !panelActive; // Toggle the panel's active state

            // Activate or deactivate the panel based on the active state
            panel.SetActive(panelActive);

            // Hide or show the specified GameObject based on the active state of the panel
            objectToHide.SetActive(!panelActive);
        }
    }
}
