using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public TMP_Text[] textElements; // Assign your TMP Text elements in the Inspector
    public int currentTextIndex = 0;
    public string nextSceneName; // Assign the name of the next scene in the Inspector

    private void Start()
    {
        ShowTextAtIndex(currentTextIndex);
    }

    public void OnNextButtonClicked()
    {
        if (currentTextIndex < textElements.Length - 1)
        {
            currentTextIndex++;
            ShowTextAtIndex(currentTextIndex);
        }
        else
        {
            LoadNextScene();
        }
    }

    private void ShowTextAtIndex(int index)
    {
        for (int i = 0; i < textElements.Length; i++)
        {
            textElements[i].gameObject.SetActive(i == index);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
