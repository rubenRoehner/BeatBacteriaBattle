using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameStateManager.Instance.gameState = GameState.IN_GAME;
        SceneManager.LoadScene(1);
    }

    public void StartTutorial()
    {
        GameStateManager.Instance.gameState = GameState.TUTORIAL;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
