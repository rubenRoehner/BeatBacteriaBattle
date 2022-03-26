using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        Reset();
    }

    private void Update()
    {
        if (GameOver && (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene(0);
            Reset();
        }
    }

    public void IsGameOver()
    {
        GameOver = true;

        //foreach (var move in GameObject.FindObjectsOfType<MovingObject>()) move.StopMove();
    }



    public void Reset()
    {
        GameOver = false;
    }
}
