using System.Collections;
using System.Collections.Generic;
using Fikhas.Audio;
using Mono.Cecil;
using UnityEngine;

public class SingoThrow : MonoBehaviour
{
    [SerializeField] GameObject stone;
    private float timer;
    private float delay;
    private GameObject target;
    private Vector3 singoThrowRange;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        singoThrowRange = new Vector3(9, 5, 0);
    }

    private void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= 4 && gameObject.GetComponent<Enemy>().currentState != EnemyState.stat)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= 1 && gameObject.GetComponent<Enemy>().currentState != EnemyState.stat)
        {
            if (gameObject.GetComponent<SingoBarong>().currentState != EnemyState.stagger)
            {
                if (gameObject.GetComponent<EnemyWalk>().isStatic == false)
                {
                    SingoThrowStone();
                    timer = 0;
                }
            }
            timer = 0;
        }
    }

    public void SingoThrowStone()
    {
        StartCoroutine(SingoThrowCo());
    }

    private IEnumerator SingoThrowCo()
    {
        SoundSystem.Instance.PlayAudio("SingoThrow", false, "s-throw");
        anim.SetBool("isThrow", true);
        Instantiate(stone, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isThrow", false);

    }
}
