using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float FastSpeed = 50F;
    public float SlowSpeed = 30F;
    private float CurrentSpeed = 50F;
    public int state = 1;
    bool changeSize = false;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        this.CurrentSpeed = SlowSpeed;
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

        if(GameManager.Instance.godmode > 0)
        {
            GameManager.Instance.godmode -= Time.deltaTime;
        }

        UpdateSpeed();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //rigidBody.velocity += new Vector2(0, Force);
            rigidBody.AddForce(new Vector2(0, CurrentSpeed));
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //rigidBody.velocity += new Vector2(-Force, 0);
            rigidBody.AddForce(new Vector2(-CurrentSpeed, 0));
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //rigidBody.velocity += new Vector2(0, -Force);
            rigidBody.AddForce(new Vector2(0, -CurrentSpeed));
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //rigidBody.velocity += new Vector2(Force, 0);
            rigidBody.AddForce(new Vector2(CurrentSpeed, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovementSmallEnemie>() != null &&
           collision.gameObject.GetComponent<MovementSmallEnemie>().State <= state)
        {
            Debug.Log(state);
            state += Mathf.RoundToInt(Mathf.Pow(2f, collision.gameObject.GetComponent<MovementSmallEnemie>().State - 1));
            changeSize = true;
            int stateOfEnemy = collision.gameObject.GetComponent<MovementSmallEnemie>().State;
            Destroy(collision.gameObject);
            GameObject.Find("EnemyManager").GetComponent<ObstacleGenerator>().Kill(stateOfEnemy);
            GameManager.Instance.SoundBigger();
        }
        else if (collision.gameObject.GetComponent<MovementSmallEnemie>() != null && GameManager.Instance.godmode <= 0)
        {
            Debug.Log(collision.gameObject.GetComponent<MovementSmallEnemie>().State + " " + state);
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateSpeed()
    {
        switch(GameManager.Instance.heartbeatController.heartBeatRate)
        {
            case HeartBeatRate.SLOW:
                this.CurrentSpeed = SlowSpeed;
                break;
            case HeartBeatRate.FAST:
                this.CurrentSpeed = FastSpeed;
                break;
        }
    }

    public void Reset()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(2.5f, 2.5f, 2f);
        state = 1;
        GameManager.Instance.godmode = 3;
    }
}
