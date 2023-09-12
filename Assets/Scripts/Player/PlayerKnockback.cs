using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : Knockback
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable"))
        {
            // other.GetComponent<Pot>().Smash();
        }
        if (other.CompareTag("EnemyBody"))
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                other.GetComponentInParent<Enemy>().currentState = EnemyState.stagger;
                other.GetComponentInParent<Enemy>().Knock(hit, knockTime, damage);
                other.GetComponentInParent<Enemy>().isHit = true;
                Vector2 difference = hit.transform.position - transform.position;
                Vector2 diff = difference.normalized;
                hit.AddForce(diff * thrust, ForceMode2D.Impulse);
            }
        }
        if (other.CompareTag("SingoBarong"))
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.GetComponentInParent<SingoBarong>().currentState != EnemyState.stat)
                {
                    other.GetComponentInParent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponentInParent<Enemy>().Knock(hit, knockTime, damage);
                    other.GetComponentInParent<Enemy>().isHit = true;
                    Vector2 difference = hit.transform.position - transform.position;
                    Vector2 diff = difference.normalized;
                    hit.AddForce(diff * thrust, ForceMode2D.Impulse);
                }
            }
        }
    }
}
