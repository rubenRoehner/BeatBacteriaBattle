using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSmallEnemie : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rid;
    float move = 6f;
    public int State = 1;
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rid.AddForce(new Vector2(Random.Range(-move, move), Random.Range(-move, move)));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<MovementSmallEnemie>() != null && collision.gameObject.GetComponent<MovementSmallEnemie>().State == State)
        {
            GameManager.Instance.Merge(gameObject, collision.gameObject);
        }
        
    }
}
