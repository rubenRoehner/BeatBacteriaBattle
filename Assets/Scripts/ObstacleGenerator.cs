using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{ 
    public GameObject Prefab;
    private static readonly int[] NumEnemies = {11, 30 , 40};
    private static readonly int[] BossSize = { 10, 12, 16 };
    private GameObject[] allEnemys;
    public float MinX = 3;
    public float MaxX = 23;
    public float MinY = 3;
    public float MaxY = 15;

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
        int sizeArray = allEnemys.Length;
        float value = 1 - ((enemyLeft - 1) / numEnemies);

        Debug.Log("enemyLeft: " + enemyLeft + ", NumEnemies: " + numEnemies + ", ArraySize: " + sizeArray + "value" + value);

        GameManager.Instance.gameUI.slider.value = value;
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = new Vector3(RandomInRange(MinX, MaxX), Random.Range(MinY, MaxY), 0f);
        return obstacle;
    }

    private GameObject CreateBoss(int level)
    {
        GameObject boss = Instantiate(Prefab);
        boss.transform.position = new Vector3(RandomInRange(MinX + 4, MaxX - 4), RandomInRange(MinY + 4, MaxY - 4), 0f);
        Debug.Log("posX: " + boss.transform.position.x + "posY" + boss.transform.position.y);
        int currentBossSize = BossSize[level];
        boss.transform.localScale = new Vector3(currentBossSize, currentBossSize, currentBossSize);
        boss.GetComponent<MovementSmallEnemie>().State = currentBossSize;
        boss.GetComponent<Animator>().SetInteger("EnemyLevel", 4);
        return boss;
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
        allEnemys = new GameObject[NumEnemies[level] + 1];
        allEnemys[0] = CreateBoss(level);

        for (int i = 1; i < allEnemys.Length; i++)
        {
            allEnemys[i] = CreateObstacle();
        }
        enemyLeft = allEnemys.Length;
        UpdateEnemiesLeft();
    }



    public static float RandomInRange(float min, float max)
    {
        return Random.value > 0.5f ?
       Random.Range(-max, -min) :
       Random.Range(min, max);
    }
}
