using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{ 
    public GameObject Prefab;
    private static readonly int NumEnemies = 10;
    private GameObject[] allEnemys;
    public float Min = -1;
    public float Max = 1;

    private int enemyLeft;
    public Text lbl_enemyLeft;


    public void Start()
    {
        CreateEnemies();
    }

    public void Kill(int stateOfEnemy)
    {
        enemyLeft -= stateOfEnemy;
        UpdateEnemiesLeft();
        if (enemyLeft <= 0)
        {
            GameManager.Instance.CompleteLevel();
        }
    }

    public void AddEnemy(int v)
    {
        enemyLeft += v;
        UpdateEnemiesLeft();
    }

    private void UpdateEnemiesLeft()
    {
        lbl_enemyLeft.text = "Enemys left: " + enemyLeft;
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = new Vector3(Random.Range(Min, Max), Random.Range(Min, Max), 0f);
        return obstacle;
    }

    public void Reset()
    {
        enemyLeft = 0;
        RemoveExistingEnemies();
        CreateEnemies();
        UpdateEnemiesLeft();
    }

    private void RemoveExistingEnemies()
    {
        foreach(GameObject enemy in allEnemys)
        {
            Destroy(enemy);
        }
    }

    private void CreateEnemies()
    {
        allEnemys = new GameObject[NumEnemies];
        allEnemys[0] = CreateObstacle();
        allEnemys[0].transform.localScale = new Vector3(7, 7, 7);
        allEnemys[0].GetComponent<MovementSmallEnemie>().State = 7;
        allEnemys[0].GetComponent<Animator>().SetInteger("EnemyLevel", 4);
        AddEnemy(7);

        for (int i = 1; i < NumEnemies; i++)
        {
            allEnemys[i] = CreateObstacle();
            AddEnemy(1);
        }

    }
}
