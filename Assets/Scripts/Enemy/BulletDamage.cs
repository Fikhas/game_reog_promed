using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : Knockback
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.GetComponent<PlayerMovement>().playerCurrentState != PlayerState.stagger && !other.GetComponent<PlayerMovement>().isHit && other.GetComponent<PlayerMovement>().playerCurrentState != PlayerState.imun)
                {
                    hit.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    other.GetComponent<PlayerMovement>().isHit = true;
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                }
                Destroy(gameObject);
            }
        }
    }
}
