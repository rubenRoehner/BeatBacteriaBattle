using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameOver = false;
    private int enemyLeft;

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

    public void Merge(GameObject go1, GameObject go2)
    {
        if(go1.active == true && go2.active == true)
        {
            go1.SetActive(false);
            go2.transform.localScale = new Vector3(go2.transform.localScale.x + 1 , go2.transform.localScale.y + 1, 2);
            go2.GetComponent<MovementSmallEnemie>().State++;
        }
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
