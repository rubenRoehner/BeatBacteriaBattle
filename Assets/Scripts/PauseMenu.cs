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
            if(GameStateManager.Instance.gameState == GameState.PAUSED)
            {
                Resume();
            }
            if(GameStateManager.Instance.gameState == GameState.IN_GAME)
            {
                Pause();
            }
        } 
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameStateManager.Instance.gameState = GameState.IN_GAME;
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameStateManager.Instance.gameState = GameState.PAUSED;
        Time.timeScale = 0f;
    }
}