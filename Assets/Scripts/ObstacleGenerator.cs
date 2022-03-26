using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour

{ 
    public GameObject Prefab;
    private int numEnemies = 5;
    private GameObject[] allEnemys;
    public float Min = -1;
    public float Max = 1;
    // Start is called before the first frame update
    void Start()
    {
        allEnemys = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++)
        {
            allEnemys[i] = CreateObstacle();
        }
    }

    private GameObject CreateObstacle()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = new Vector3(Random.Range(Min, Max), Random.Range(Min, Max), 0f);
        return obstacle;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
