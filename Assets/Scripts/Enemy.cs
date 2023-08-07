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
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public HealthBar healthBar;
    public GameObject currentEnemy;
    // public FloatValue maxHealth;

    // Start is called before the first frame update
    void Awake()
    {
        currentState = EnemyState.idle;
        // health = maxHealth.initialValue;
        healthBar.SetMaxValue(health);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
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
