using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    stat
}

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathSoundEffect;
    [SerializeField] GameObject enemyKnockSounEffect;
    [SerializeField] GameObject deathEffect;
    public EnemyState currentState;
    public HealthBar healthBar;
    private SpriteRenderer sprite;
    public float health;
    private float timer;
    private float delay = 3f;
    private string color = "red";
    [HideInInspector] public Signal deathSignal;
    [HideInInspector] public bool isHit;

    void Awake()
    {
        healthBar.SetMaxValue(health);
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isHit && timer < delay)
        {
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
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
        {
            if (deathSoundEffect != null)
            {
                Instantiate(deathSoundEffect);
            }
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
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
        Instantiate(enemyKnockSounEffect);
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
