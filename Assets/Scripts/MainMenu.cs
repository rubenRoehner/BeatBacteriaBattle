using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGameMenu()
    {
        GameManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
