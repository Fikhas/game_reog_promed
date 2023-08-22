using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable"))
        {
            // other.GetComponent<Pot>().Smash();
        }
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                other.GetComponent<Enemy>().isHit = true;
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }
}
