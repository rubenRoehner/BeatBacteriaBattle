using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{ 
    public GameObject Prefab;
    private static readonly int[] NumEnemies = {11, 30 , 40};
    private static readonly int[] BossSize = { 10, 12, 16 };
    private GameObject[] allEnemys;
    public float Min = -1;
    public float Max = 1;

    private float enemyLeft = 0f;


    public void Start()
    {
        CreateEnemies(0);
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

    private void UpdateEnemiesLeft()
    {
        int numEnemies = NumEnemies[GameManager.Instance.currentLevel];
        float value = 1 - (enemyLeft / numEnemies);
        Debug.Log("enemyLeft: " + enemyLeft + "NumEnemies" + numEnemies);
        GameManager.Instance.gameUI.slider.value = value;
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = new Vector3(Random.Range(Min, Max), Random.Range(Min, Max), 0f);
        return obstacle;
    }

    public void Reset(int level)
    {
        enemyLeft = 0;
        RemoveExistingEnemies();
        CreateEnemies(level);
        UpdateEnemiesLeft();
    }

    private void RemoveExistingEnemies()
    {
        foreach(GameObject enemy in allEnemys)
        {
            Destroy(enemy);
        }
    }

    private void CreateEnemies(int level)
    {
        allEnemys = new GameObject[NumEnemies[level]];
        allEnemys[0] = CreateObstacle();
        int currentBossSize = BossSize[GameManager.Instance.currentLevel];
        allEnemys[0].transform.localScale = new Vector3(currentBossSize, currentBossSize, currentBossSize);
        allEnemys[0].GetComponent<MovementSmallEnemie>().State = currentBossSize;
        allEnemys[0].GetComponent<Animator>().SetInteger("EnemyLevel", 4);

        for (int i = 1; i < NumEnemies[level]; i++)
        {
            allEnemys[i] = CreateObstacle();
        }
        enemyLeft = NumEnemies[GameManager.Instance.currentLevel];
        UpdateEnemiesLeft();
    }
}
