using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float Force = 50f;
    private int state = 1;
    bool changeSize = false;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void ChangeSize(int i)
    {
        if (changeSize)
        {
            switch (i)
            {
                case 1: transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, 2); break;
                case 2: transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, 2); break;
                default:
                    transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, 2); break;
            }

            changeSize = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        ChangeSize(state);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
        if (collision.gameObject.GetComponent<MovementSmallEnemie>() != null &&
           collision.gameObject.GetComponent<MovementSmallEnemie>().State <= state)
        {
            state++;
            changeSize = true;
            GameObject.Find("EnemyManager").GetComponent<ObstacleGenerator>().Kill(collision.gameObject.GetComponent<MovementSmallEnemie>().State);
            collision.gameObject.SetActive(false);

        }
        else if (collision.gameObject.GetComponent<MovementSmallEnemie>() != null)
        {  SceneManager.LoadScene(2); }
    }
}
