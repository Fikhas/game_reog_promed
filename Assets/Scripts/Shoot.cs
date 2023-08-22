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
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            Shooting();
        }
    }

    private void Shooting()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
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
