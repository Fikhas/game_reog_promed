using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKnockback : Knockback
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
			if (hit != null)
			{
				if (other.GetComponent<Player>().playerCurrentState != PlayerState.stagger && !other.GetComponent<Player>().isHit && other.GetComponent<Player>().playerCurrentState != PlayerState.imun)
				{
					hit.GetComponent<Player>().playerCurrentState = PlayerState.stagger;
					other.GetComponent<Player>().Knock(knockTime, damage);
					other.GetComponent<Player>().isHit = true;
					Vector2 difference = hit.transform.position - transform.position;
					difference = difference.normalized * thrust;
					hit.AddForce(difference, ForceMode2D.Impulse);
				}
			}
		}
	}
}
