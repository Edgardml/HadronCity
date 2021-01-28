using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : Movement
{
    Rigidbody2D rb2d;
    float h, v;
    WeaponController wc;
    [SerializeField]
    LayerMask blockingLayer;
    [SerializeField]
    float agroRange;
    enum Direction { Up, Down, Left, Right };
    GameObject player;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        RandomDirection();
        wc = GetComponentInChildren<WeaponController>();
        Invoke("FireWhenWanted", Random.Range(1f, 5f));
        player = GameObject.Find("Player");
    }

    public void RandomDirection()
    {
        CancelInvoke("RandomDirection");

        List<Direction> lottery = new List<Direction>();
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(1, 0), blockingLayer))
        {
            lottery.Add(Direction.Right);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-1, 0), blockingLayer))
        {
            lottery.Add(Direction.Left);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, 1), blockingLayer))
        {
            lottery.Add(Direction.Up);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -1), blockingLayer))
        {
            lottery.Add(Direction.Down);
        }

        Direction selection = lottery[Random.Range(0, lottery.Count)];
        if (selection == Direction.Up)
        {
            v = 1;
            h = 0;
        }
        if (selection == Direction.Down)
        {
            v = -1;
            h = 0;
        }
        if (selection == Direction.Right)
        {
            v = 0;
            h = 1;
        }
        if (selection == Direction.Left)
        {
            v = 0;
            h = -1;
        }
        Invoke("RandomDirection", Random.Range(3, 6));
    }

    void FireWhenWanted()
    {
        wc.Fire();
        Invoke("FireWhenWanted", Random.Range(1f, 5f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomDirection();
    }

    private void FixedUpdate()
    {
        if (v != 0 && isMoving == false) StartCoroutine(MoveVertical(v, rb2d));
        else if (h != 0 && isMoving == false) StartCoroutine(MoveHorizontal(h, rb2d));
        if (CanSeePlayer(agroRange, h, v))
        {
            wc.Fire();
        }
    }

    private bool CanSeePlayer(float distance, float h, float v)
    {
        bool can = false;
        var castDistance = distance;


        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position, 1 << LayerMask.NameToLayer("PlayerTank") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Boundary")); ;
        if (h > 0 && v == 0)
        {
           hit = Physics2D.Linecast(transform.position, wc.gameObject.transform.position + Vector3.right * distance, 1 << LayerMask.NameToLayer("PlayerTank") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Boundary"));
        }
        else if (h < 0 && v == 0)
        {
            hit = Physics2D.Linecast(transform.position, wc.gameObject.transform.position + Vector3.right * -distance, 1 << LayerMask.NameToLayer("PlayerTank") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Boundary"));
        }
        else if (h == 0 && v > 0)
        {
            hit = Physics2D.Linecast(transform.position, wc.gameObject.transform.position + Vector3.up * distance, 1 << LayerMask.NameToLayer("PlayerTank") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Boundary"));
        }
        else if (h == 0 && v < 0)
        {
            hit = Physics2D.Linecast(transform.position, wc.gameObject.transform.position + Vector3.up * -distance, 1 << LayerMask.NameToLayer("PlayerTank") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Boundary"));
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                can = true;
            }
            else
            {
                can = false;
            }

            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
            Debug.DrawLine(transform.position, wc.gameObject.transform.position + Vector3.right * distance, Color.yellow);

        return can;
    }
}
