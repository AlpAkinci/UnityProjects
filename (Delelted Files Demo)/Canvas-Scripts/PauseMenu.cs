using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuUI2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
        public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        pauseMenuUI2.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;

    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene("MainMenu");
        GameIsPaused = false;
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
