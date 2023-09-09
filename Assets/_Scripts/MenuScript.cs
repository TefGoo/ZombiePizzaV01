using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Intro()
    {
        SceneManager.LoadScene("IntroDialogues");
    }
    public void Back()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
