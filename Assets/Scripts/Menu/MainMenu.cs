using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screenOptions;
    private GameObject prevScreen;
    private GameObject activeScreen;
    public UIManager UIM;
    public void SelectLevel() 
    {
        activeScreen = screen2;
        prevScreen = screen1;
        //SceneManager.LoadScene("Level_Selector");
        prevScreen.SetActive(false);
        activeScreen.SetActive(true);
    }

    public void GoToLevel(int id)
    {
        string level = id.ToString();
        SceneManager.LoadScene("Level_" + level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InstanceOptions()
    {
        Debug.Log("Click");
        prevScreen = screen1;
        activeScreen = screenOptions;
        prevScreen.SetActive(false);
        activeScreen.SetActive(true);
    }
    public void GoBack()
    {
        activeScreen.SetActive(false);
        prevScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        UIM.ResumeGame();
    }
}
