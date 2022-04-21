using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(System.Enum.GetName(typeof(GameState), GameManager.Instance.gameState));
            if(GameManager.Instance.gameState == GameState.PAUSED)
            {
                Resume();
            }
            if(GameManager.Instance.gameState == GameState.IN_GAME)
            {
                Pause();
            }
        } 
    }

    public void Resume()
    {
        Debug.Log("Resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.gameState = GameState.IN_GAME;
    }

    public void ShowMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.gameState = GameState.PAUSED;
    }
}
