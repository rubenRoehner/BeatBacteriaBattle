using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Force = 50f;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //rigidBody.velocity += new Vector2(0, Force);
            rigidBody.AddForce(new Vector2(0, Force));
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //rigidBody.velocity += new Vector2(-Force, 0);
            rigidBody.AddForce(new Vector2(-Force, 0));
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //rigidBody.velocity += new Vector2(0, -Force);
            rigidBody.AddForce(new Vector2(0, -Force));
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //rigidBody.velocity += new Vector2(Force, 0);
            rigidBody.AddForce(new Vector2(Force, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovementSmallEnemie>() != null) { collision.gameObject.SetActive(false); }
    }
}
