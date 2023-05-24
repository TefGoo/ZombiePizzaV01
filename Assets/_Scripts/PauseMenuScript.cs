using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu GameObject
    public Button resumeButton; // Reference to the button that resumes the game
    private bool isPaused; // Flag to track the pause state

    private void Start()
    {
        // Add an event listener to the resume button
        resumeButton.onClick.AddListener(() => ResumeGame());
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

    public void ResumeGame()
    {
        TogglePauseState(); // Call the function to resume the game
    }
}
