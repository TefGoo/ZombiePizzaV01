using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
