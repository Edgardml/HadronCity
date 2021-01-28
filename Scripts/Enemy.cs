using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite spawn1;
    public Sprite spawn2;
    public Sprite spawn3;

    public Sprite sprUp;
    public Sprite sprDown;
    public Sprite sprLeft;
    public Sprite sprRight;
    public Sprite sprExplosion;
    public GameObject bulletGO;

    public float speed = 4f;

    private string direction;
    private float directionTimeChange;
    private float shootingTime;
    private Vector3 currentPos;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spawn1;

        int temp = Random.Range(1, 5);
        switch (temp)
        {
            case 1:
                direction = "up";
                break;
            case 2:
                direction = "down";
                break;
            case 3:
                direction = "left";
                break;
            case 4:
                direction = "right";
                break;
        }

        directionTimeChange = Random.Range(3f, 5f);
        shootingTime = Random.Range(0, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            changeDirection();
            doShoot();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

    }

    public void changeDirection()
    {
        directionTimeChange -= Time.deltaTime;
        if (directionTimeChange < 0)
        {
            directionTimeChange = Random.Range(3F, 6f);
            int temp = Random.Range(1, 5);
            switch (temp)
            {
                case 1:
                    direction = "up";
                    break;
                case 2:
                    direction = "down";
                    break;
                case 3:
                    direction = "left";
                    break;
                case 4:
                    direction = "right";
                    break;
            }
        }
        doMove();
    }

    public void doShoot()
    {
        shootingTime -= Time.deltaTime;
        if (shootingTime < 0)
        {
            shootingTime = 5f;
            GameObject bullet;
            bullet = Instantiate(bulletGO, GetComponent<Transform>().position, Quaternion.identity);
            // bullet.GetComponent<Bullet>().dirBullet = direction;
            // bullet.GetComponent<Bullet>().owner = "enemy";
            Destroy(bullet, 5f);
        }
    }

    public void doMove()
    {
        switch (direction)
        {
            case "up": //Up
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                GetComponent<SpriteRenderer>().sprite = sprUp;
                break;
            case "down": //Down
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                GetComponent<SpriteRenderer>().sprite = sprDown;
                break;
            case "left": //Left
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                GetComponent<SpriteRenderer>().sprite = sprLeft;
                break;
            case "right"://Right
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                GetComponent<SpriteRenderer>().sprite = sprRight;
                break;

            default:
                break;
        }

        currentPos = GetComponent<Transform>().position;
        if (currentPos.x > 10.5f && direction == "right") direction = "left";
        if (currentPos.x < -10.5f && direction == "left") direction = "right";
        if (currentPos.y > 9.4f && direction == "up") direction = "down";
        if (currentPos.y < -9.4f && direction == "down") direction = "up";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (direction == "right")
            {
                int temp = Random.Range(1, 4);
                switch (temp)
                {
                    case 1:
                        direction = "up";
                        break;
                    case 2:
                        direction = "down";
                        break;
                    case 3:
                        direction = "left";
                        break;
                }
            }

            if (direction == "left")
            {
                int temp = Random.Range(1, 4);
                switch (temp)
                {
                    case 1:
                        direction = "up";
                        break;
                    case 2:
                        direction = "down";
                        break;
                    case 3:
                        direction = "right";
                        break;
                }
            }
            if (direction == "up")
            {
                int temp = Random.Range(1, 4);
                switch (temp)
                {
                    case 1:
                        direction = "right";
                        break;
                    case 2:
                        direction = "down";
                        break;
                    case 3:
                        direction = "left";
                        break;
                }
            }
            if (direction == "down")
            {
                int temp = Random.Range(1, 4);
                switch (temp)
                {
                    case 1:
                        direction = "up";
                        break;
                    case 2:
                        direction = "right";
                        break;
                    case 3:
                        direction = "left";
                        break;
                }
            }
            doMove();
        }
    }
}
