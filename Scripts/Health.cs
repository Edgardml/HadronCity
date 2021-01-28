using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int actualHealth;
    public int currentHealth;
    Animator anime;
    Rigidbody2D rb2d;
    Image fullHealth, midHealth, lowHealth;

    public bool isPlayer = false;

    void Start()
    {

        anime = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        SetHealth();
    }
    public void TakeDamage()
    {
        currentHealth--;
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        GPM.UpdatePlayerHealth(currentHealth);
        if (currentHealth <= 0)
        {
            rb2d.velocity = Vector2.zero;
            anime.SetTrigger("Killed");
        }
        
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }

    public void SetHealth()
    {
            currentHealth = actualHealth;
    }

    public void SetInvincible()
    {
        currentHealth = 1000;
    }

    void Death()
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer();
        }
        else
        {
            if (gameObject.CompareTag("enemy")) GameManager.smallTanksDestroyed++;
        }
        Destroy(gameObject);
    }
}
