using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable") && this.CompareTag("Player"))
        {
            // other.GetComponent<Pot>().Smash();
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.CompareTag("Enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                else if (other.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().playerCurrentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }

    //     private void OnTriggerEnter2D(Collider2D other)
    //     {
    //         if (other.gameObject.CompareTag("Enemy"))
    //         {
    //             Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
    //             if (enemy != null)
    //             {
    //                 StartCoroutine(KnockCoroutine(enemy));
    //             }
    //         }
    //     }

    //     private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    //     {
    //         Vector2 forceDirection = enemy.transform.position - transform.position;
    //         Vector2 force = forceDirection.normalized * thrust;

    //         enemy.velocity = force;
    //         yield return new WaitForSeconds(.3f);

    //         enemy.velocity = new Vector2();
    //     }
}
