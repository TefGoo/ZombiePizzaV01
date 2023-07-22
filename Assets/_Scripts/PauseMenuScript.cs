using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu GameObject
    public GameObject controlsPanel; // Reference to the controls panel GameObject
    public Button resumeButton; // Reference to the button that resumes the game
    public Button controlsButton; // Reference to the button that shows controls
    public Button backButton; // Reference to the button that goes back to pause menu

    private bool isPaused; // Flag to track the pause state

    private void Start()
    {
        // Add an event listener to the resume button
        resumeButton.onClick.AddListener(() => ResumeGame());

        // Add an event listener to the controls button
        controlsButton.onClick.AddListener(() => ShowControlsPanel());

        // Add an event listener to the back button in controls panel
        backButton.onClick.AddListener(() => ShowPauseMenu());
    }

    private void Update()
    {
        // Check for Esc key press on the keyboard or Start button press on the controller
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            TogglePauseState();
        }
    }

    public void TogglePauseState()
    {
        isPaused = !isPaused; // Toggle the pause state

        // Activate or deactivate the pause menu based on the pause state
        pauseMenu.SetActive(isPaused);

        // Time.timeScale controls the time scale of the game. Set it to 0 to pause the game, and 1 to resume.
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ShowControlsPanel()
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        controlsPanel.SetActive(true); // Show the controls panel
    }

    public void ShowPauseMenu()
    {
        controlsPanel.SetActive(false); // Hide the controls panel
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        TogglePauseState(); // Call the function to resume the game
    }
}
