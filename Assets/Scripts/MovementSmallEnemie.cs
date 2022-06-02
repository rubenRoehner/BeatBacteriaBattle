using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSmallEnemie : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rid;
    private float currentSpeed = 4f;
    public int State = 1;
    public float SlowSpeed = 4F;
    public float HighSpeed = 20F;

    private float replaceTimeCap = 3;
    

    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (State <= GameManager.Instance.playerController.state)
        {
            rid.AddForce(new Vector2(Random.Range(-currentSpeed, currentSpeed), Random.Range(-currentSpeed, currentSpeed)));
        }
        else if (State > GameManager.Instance.playerController.state)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.playerController.transform.position, SlowSpeed * Time.deltaTime);
        }

        if(replaceTimeCap > 0)
        {
            replaceTimeCap -= Time.deltaTime;
        } 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<MovementSmallEnemie>() != null && collision.gameObject.GetComponent<MovementSmallEnemie>().State == State && State < 3)
        {
            GameManager.Instance.Merge(gameObject, collision.gameObject);
        }

        if(collision.gameObject.tag == "respawnCollider" && replaceTimeCap > 0)
        {
            transform.position = new Vector3(5, 5, 5f);
        }

    }

    private void UpdateSpeed()
    {
        switch (GameManager.Instance.heartbeatController.heartBeatRate)
        {
            case HeartBeatRate.SLOW:
                this.currentSpeed = SlowSpeed;
                break;
            case HeartBeatRate.FAST:
                this.currentSpeed = HighSpeed;
                break;
        }
    }
}
