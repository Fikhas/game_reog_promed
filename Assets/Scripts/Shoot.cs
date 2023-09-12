using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    float timer;

    private void Update()
    {
        if (Vector3.Distance(gameObject.GetComponent<EnemyWalk>().target.transform.position, transform.position) <= gameObject.GetComponent<EnemyWalk>().attackRadius)
        {
            timer += Time.deltaTime;
            Debug.Log("waktu berjalan");
        }
        if (timer > 1 && Vector3.Distance(gameObject.GetComponent<EnemyWalk>().target.transform.position, transform.position) <= gameObject.GetComponent<EnemyWalk>().attackRadius)
        {
            timer = 0;
            StartCoroutine(ShootingCo());
        }
    }

    private void Shooting()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private IEnumerator ShootingCo()
    {
        Debug.Log("enemy attack");
        gameObject.GetComponent<Animator>().SetBool("isAttack", true);
        Instantiate(bullet, transform.Find("BulletPos").position, Quaternion.identity);
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
