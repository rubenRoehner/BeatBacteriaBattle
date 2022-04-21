using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void ShowMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
