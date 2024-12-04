using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TutorialMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TutorialMenu.SetActive(true);
        }
    }
    public void closeTutorial()
    {
        TutorialMenu.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
