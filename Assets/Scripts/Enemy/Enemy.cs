using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public HealthBar healthBar;
    private SpriteRenderer sprite;
    public Signal deathSignal;
    public static Enemy sharedInstance;
    public float health;
    public string enemyName;
    public bool isHit;
    private float timer;
    private float delay = 3f;
    private string color = "red";

    void Awake()
    {
        healthBar.SetMaxValue(health);
        sprite = GetComponent<SpriteRenderer>();
        sharedInstance = this;
    }

    void Update()
    {
        if (isHit && timer < delay)
        {
            // playerCurrentState = PlayerState.hit;
            if (color == "red" && Mathf.Round(timer * 10.0f) * 0.1f % .4f != 0f)
            {
                sprite.material.SetColor("_Color", Color.red);
                color = "white";
            }
            else if (color == "white" && Mathf.Round(timer * 10.0f) * 0.1f % .4f == 0f)
            {
                sprite.material.SetColor("_Color", Color.white);
                color = "red";
            }
            timer += Time.deltaTime;
        }
        else
        {
            isHit = false;
            timer = 0f;
            sprite.material.SetColor("_Color", Color.white);
            // playerCurrentState = PlayerState.walk;
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
        {
            Destroy(gameObject);
            deathSignal.Raise();
        }
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        if (currentState == EnemyState.stagger)
        {
            TakeDamage(damage);
        }
        healthBar.SetHealth(health);
    }

    private IEnumerator KnockCo(Rigidbody2D enemy, float knockTime)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }
}
