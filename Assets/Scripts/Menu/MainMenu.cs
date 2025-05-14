using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nameScene;
    public GameObject pauseMenu;

    public void PlayGame(string sceneName)
    {
        LoadGame.Instance.wasLoaded = false;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
