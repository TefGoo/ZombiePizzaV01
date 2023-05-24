using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene()
    {
        // Replace "SceneName" with the name of the scene you want to load
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
