using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


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
            go1.SetActive(false);
            go2.transform.localScale = new Vector3(go2.transform.localScale.x + 1 , go2.transform.localScale.y + 1, 2);
            go2.GetComponent<MovementSmallEnemie>().State *= 2;
        }
    }

    public void GameOver()
    {
        HeartbeatController.Instance.Pause();
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        HeartbeatController.Instance.Play();
    }

    public void Restart()
    {
        StartGame();
        SceneManager.LoadScene(1);
    }

    private void Update()
    {

    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }

}
