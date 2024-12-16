using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject shootSoundEffect;
    public GameObject bullet;
    public Transform bulletPos;
    float timer;

    private void Update()
    {
        if (Vector3.Distance(gameObject.GetComponent<EnemyWalk>().target.transform.position, transform.position) <= gameObject.GetComponent<EnemyWalk>().attackRadius)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1 && Vector3.Distance(gameObject.GetComponent<EnemyWalk>().target.transform.position, transform.position) <= gameObject.GetComponent<EnemyWalk>().attackRadius)
        {
            timer = 0;
            StartCoroutine(ShootingCo());
        }
    }

    private IEnumerator ShootingCo()
    {
        Instantiate(shootSoundEffect);
        gameObject.GetComponent<Animator>().SetBool("isAttack", true);
        Instantiate(bullet, transform.Find("BulletPos").position, Quaternion.identity);
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<Animator>().SetBool("isAttack", false);

    }
}
