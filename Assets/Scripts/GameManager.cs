using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState = GameState.IN_GAME;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public HeartbeatController heartbeatController;

    private int currentLevel = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        StartGame();
    }



    public void Merge(GameObject go1, GameObject go2)
    {
        if(go1.active == true && go2.active == true)
        {
            Destroy(go1);
            go2.transform.localScale = new Vector3(go2.transform.localScale.x + 1 , go2.transform.localScale.y + 1, 2);
            go2.GetComponent<MovementSmallEnemie>().State *= 2;
            int state = go2.GetComponent<MovementSmallEnemie>().State;
            int enemyLevel = (int) Mathf.Log(state, 2) + 1;
            go2.GetComponent<Animator>().SetInteger("EnemyLevel", enemyLevel);
        }
    }

    public void GameOver()
    {
        gameState = GameState.GAME_OVER;
        gameOverUI.SetActive(true);
    }

    public void StartGame()
    {
        gameState = GameState.IN_GAME;
        heartbeatController.Play();
    }

    public void Restart()
    {
        gameState = GameState.IN_GAME;
        gameOverUI.SetActive(false);
        StartGame();
    }

    public void CompleteLevel()
    {
        gameState = GameState.LEVEL_COMPLETE;
        completeLevelUI.SetActive(true);
        heartbeatController.Pause();
        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        Restart();
    }

    private void Update()
    {
        Debug.Log(System.Enum.GetName(typeof(GameState), gameState));
    }
}

public enum GameState
{
    IN_GAME, PAUSED, LEVEL_COMPLETE, GAME_OVER
}
