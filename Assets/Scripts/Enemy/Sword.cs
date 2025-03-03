using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] GameObject swordSoundEffect;
	private float timer;
	private bool isCounting;

	private void Update()
	{
		if (isCounting && gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger)
		{
			timer += Time.deltaTime;
		}
		if (timer > .3f && gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger && gameObject.GetComponent<Enemy>().currentState != EnemyState.stat)
		{
			timer = 0;
			Slashing();
		}
		if (gameObject.GetComponent<Enemy>().currentState == EnemyState.stat)
		{
			timer = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isCounting = true;
			timer = 0;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isCounting = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isCounting = false;
			timer = 0;
		}
	}

	private void Slashing()
	{
		if (gameObject.GetComponent<EnemyWalk>().isStatic == false)
		{
			Debug.Log("Enterting slashing");
			attackCo = StartCoroutine(AttackCo());
		}
	}

	private Coroutine attackCo;
	private IEnumerator AttackCo()
	{
		if (attackCo != null)
		{
			StopCoroutine(attackCo);
			attackCo = null;
		}

		if (swordSoundEffect != null)
		{
			Instantiate(swordSoundEffect);
		}

		gameObject.GetComponent<Animator>().SetBool("isAttack", true);
		yield return new WaitForSeconds(.3f);
		gameObject.GetComponent<Animator>().SetBool("isAttack", false);
	}
}
