using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timer;
    private bool isCounting;

    private void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)
        {
            timer = 0;
            Slashing();
        }
        // Debug.Log(isCounting);
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








    // private float timer;
    // [SerializeField] float delay;
    // [SerializeField] GameObject bullet;
    // [SerializeField] float moveSpeed;

    // private void Update()
    // {
    //     if (timer > delay)
    //     {
    //         InstantiateBullet();
    //     }
    //     else if (timer < delay)
    //     {
    //         timer += Time.deltaTime;

    //     }
    //     else
    //     {
    //         timer = 0;
    //     }
    // }

    // private void InstantiateBullet()
    // {
    //     // GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
    //     // if (bullet != null)
    //     // {
    //     //     bullet.transform.position = gameObject.transform.position;
    //     //     bullet.transform.rotation = gameObject.transform.rotation;
    //     //     bullet.SetActive(true);
    //     // }
    //     GameObject projectile = Instantiate(bullet, transform.position, transform.rotation);
    //     projectile.GetComponent<Rigidbody2D>().velocity = transform.forward * moveSpeed;
    // }
}
