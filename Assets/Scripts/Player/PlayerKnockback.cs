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
        // Debug.Log(other);
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
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }
}
