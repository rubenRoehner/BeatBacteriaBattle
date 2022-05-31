using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{
    public GameObject obs;
    public GameObject obs1;
    public GameObject obs2;
    public GameObject level0;


    public GameObject Prefab;
    private static readonly int[] NumEnemies = {11, 30 , 40};
    public static readonly int[] BossSize = { 8, 13, 16 };
    private GameObject[] allEnemys;
    public float MinX = 8;
    public float MaxX = 20;
    public float MinY = 7;
    public float MaxY = 13;

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
        float value = 1 - ((enemyLeft - 1) / numEnemies);

        GameManager.Instance.gameUI.slider.value = value;

        if(enemyLeft == 1)
        {
            GameManager.Instance.checkForBoss();
        }
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);

        obstacle.transform.position = new Vector3(((int) Random.Range(0, 1) <= 0.5 ? -1 : 1) * RandomInRange(MinX, MaxX), ((int)Random.Range(0, 1) <= 0.5 ? -1 : 1) * Random.Range(MinY, MaxY), 0f);
        return obstacle;
    }


    private GameObject CreateBoss(int level)
    {
        GameObject boss = Instantiate(Prefab);
        boss.transform.position = new Vector3(Random.Range(MinX + 5, MaxX), Random.Range(MinY + 5, MaxY), 0f);
        Debug.Log("posX: " + boss.transform.position.x + "posY" + boss.transform.position.y);
        int currentBossSize = BossSize[level];
        boss.transform.localScale = new Vector3(currentBossSize , currentBossSize  , currentBossSize);
        boss.GetComponent<MovementSmallEnemie>().State = NumEnemies[level];
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

            case 1: obs.SetActive(true); obs1.SetActive(false); obs2.SetActive(false); level0.SetActive(false); break ;

            case 2: obs.SetActive(true); obs1.SetActive(true); obs2.SetActive(true); level0.SetActive(false); break;


            default: obs.SetActive(true); obs1.SetActive(true); obs2.SetActive(true); level0.SetActive(false); break;
        

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
