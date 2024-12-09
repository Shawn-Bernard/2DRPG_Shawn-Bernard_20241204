using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject gameOverScreen;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            tutorialMenu.SetActive(true);
        }
        if (gameOverScreen.activeSelf == true || tutorialMenu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void closeTutorial()
    {
        tutorialMenu.SetActive(false);
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
