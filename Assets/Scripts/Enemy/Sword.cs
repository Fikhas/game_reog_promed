using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timer;
    private bool isCounting;

    private void Update()
    {
        if (isCounting && gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.5f && gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger && gameObject.GetComponent<Enemy>().currentState != EnemyState.stat)
        {
            timer = 0;
            Slashing();
        }
        if (gameObject.GetComponent<Enemy>().currentState == EnemyState.stagger && gameObject.GetComponent<Enemy>().currentState == EnemyState.stat)
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
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        gameObject.GetComponent<Animator>().SetBool("isAttack", true);
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<Animator>().SetBool("isAttack", false);
    }
}
