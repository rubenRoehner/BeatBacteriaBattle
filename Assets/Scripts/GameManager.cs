using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public HeartbeatController heartbeatController;
    public ObstacleGenerator obstacleGenerator;
    public PlayerController playerController;
    public Tutorial tutorial;
    public GameUI gameUI;

    public int currentLevel = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        if(GameStateManager.Instance.gameState == GameState.TUTORIAL)
        {
            tutorial.StartTutorial();
        } else
        {
            StartGame();
        }
    }

    public void Merge(GameObject go1, GameObject go2)
    {
        if(go1.activeSelf == true && go2.activeSelf == true)
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
        GameStateManager.Instance.gameState = GameState.GAME_OVER;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        GameStateManager.Instance.gameState = GameState.IN_GAME;
    }

    public void Restart()
    {
        GameStateManager.Instance.gameState = GameState.IN_GAME;
        gameOverUI.SetActive(false);
        completeLevelUI.SetActive(false);
        obstacleGenerator.Reset(currentLevel);
        playerController.Reset();
        StartGame();
    }

    public void CompleteLevel()
    {
        GameStateManager.Instance.gameState = GameState.LEVEL_COMPLETE;
        completeLevelUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        if(currentLevel > 2)
        {
            SceneManager.LoadScene(1);
        } else
        {
            gameUI.UpdateLevelLabel(currentLevel + 1);
            Restart();
        }
    }

}
