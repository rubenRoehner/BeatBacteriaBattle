using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{
    public GameObject obs;
    public GameObject obs1;
    public GameObject obs2;


    public GameObject Prefab;
    private static readonly int[] NumEnemies = {11, 30 , 40};
    public static readonly int[] BossSize = { 10, 20, 16 };
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
        boss.transform.localScale = new Vector3(currentBossSize - 3, currentBossSize -3 , currentBossSize);
        boss.GetComponent<MovementSmallEnemie>().State = currentBossSize;
        boss.GetComponent<MovementSmallEnemie>().SlowSpeed = 2f;
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

        switch (level)
        {
            case 0: obs.SetActive(false); obs1.SetActive(false); obs2.SetActive(false); break;

            case 1: obs.SetActive(true); obs1.SetActive(false); obs2.SetActive(false); break;

            case 2: obs.SetActive(true); obs1.SetActive(true); obs2.SetActive(true); break;


            default: obs.SetActive(true); obs1.SetActive(true); obs2.SetActive(true); break;
        

    }

                UpdateEnemiesLeft();
    }



    public static float RandomInRange(float min, float max)
    {
        return Random.value > 0.5f ?
       Random.Range(-max, -min) :
       Random.Range(min, max);
    }
}
