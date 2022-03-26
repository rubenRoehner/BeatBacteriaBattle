using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSmallEnemie : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rid;
    float move = 6f;
    public GameObject Prefab;
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rid.AddForce(new Vector2(Random.Range(-move, move), Random.Range(-move, move)));
    }
    private GameObject CreateNext()
    {
        var obstacle = Instantiate(Prefab);
        obstacle.transform.position = transform.position;
        return obstacle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision.gameObject.SetActive(false);

    }
}
