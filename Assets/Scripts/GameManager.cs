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

    public void Merge(GameObject go1, GameObject go2)
    {
        if(go1.active == true && go2.active == true)
        {
            go1.SetActive(false);
            go2.transform.localScale = new Vector3(2 ,2, 2);
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
