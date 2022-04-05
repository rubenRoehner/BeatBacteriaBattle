using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour

{ 
    public GameObject Prefab;
    private int numEnemies = 30;
    private GameObject[] allEnemys;
    public float Min = -1;
    public float Max = 1;

    public int enemyLeft;
    public Text lbl_enemyLeft;


    public void Start()
    {
        allEnemys = new GameObject[numEnemies];
        allEnemys[0] = CreateObstacle();
        allEnemys[0].transform.localScale = new Vector3(7, 7, 7);
        allEnemys[0].GetComponent<MovementSmallEnemie>().State = 7;
        AddEnemy(7);

        for (int i = 1; i < numEnemies; i++)
        {
            allEnemys[i] = CreateObstacle();
            AddEnemy(1);
        }

    }
    public void Kill(int stateOfEnemy)
    {
        enemyLeft -= stateOfEnemy;
        UpdateText_enemysLeft();
        if (enemyLeft == 0)
        {
            GameManager.Instance.gameOver();
        }


    }

    public void AddEnemy(int v)
    {
        enemyLeft += v;
        UpdateText_enemysLeft();
    }
    private void UpdateText_enemysLeft()
    {
        lbl_enemyLeft.text = "Enemys left: " + enemyLeft;
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = new Vector3(Random.Range(Min, Max), Random.Range(Min, Max), 0f);
        return obstacle;
    }
}
