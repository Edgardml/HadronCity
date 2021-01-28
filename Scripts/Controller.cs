using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator animator;

    public Sprite sprExplosion;
    public GameObject bulletGO;
    public float speed = 3f;

    public int lifeCounter = 2;

    private string dir = "up";
    private bool dead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
            Move();
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void Animation(string stateName)
    {
        animator.Play(stateName);
    }

    public void Move()
    {
        if (Input.GetKey("up"))
        {
            // transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            Animation("Up");
            dir = "up";
        }
        else if (Input.GetKey("down"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            Animation("Down");
            dir = "down";
        }
        else if (Input.GetKey("left"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            Animation("Left");
            dir = "left";
        }
        else if (Input.GetKey("right"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            Animation("Right");
            dir = "right";
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet;
            bullet = Instantiate(bulletGO, GetComponent<Transform>().position, Quaternion.identity);
            bullet.GetComponent<Bullet>().dirBullet = dir;
            if (dir == "up") bullet.GetComponent<SpriteRenderer>().flipY = false;
            if (dir == "down") bullet.GetComponent<SpriteRenderer>().flipY = true;
            if(dir == "right") bullet.GetComponent<SpriteRenderer>().sprite = bullet.GetComponent<Bullet>().horizontalSprite;
            if (dir == "left")
            {
                bullet.GetComponent<SpriteRenderer>().sprite = bullet.GetComponent<Bullet>().horizontalSprite;
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }
            bullet.GetComponent<Bullet>().owner = "player";
            Destroy(bullet, 5f);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.owner == "enemy")
            {
                lifeCounter -= 1;
                if(lifeCounter <= 0)
                {
                    GetComponent<SpriteRenderer>().sprite = sprExplosion;
                    dead = true;
                    Destroy(this.gameObject, 1f);
                }
            }
        */

        if (collision.gameObject.CompareTag("wall"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
