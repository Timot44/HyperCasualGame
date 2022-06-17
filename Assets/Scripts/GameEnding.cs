using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public string url;
    public GameObject panelEnd, panelCredits;
    
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }

    public void GoToCredits()
    {
        panelCredits.SetActive(true);
        panelEnd.SetActive(false);
    }
    public void GoToEnd()
    {
        panelCredits.SetActive(false);
        panelEnd.SetActive(true);
    }
}
