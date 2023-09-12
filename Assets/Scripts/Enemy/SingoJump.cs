using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingoJump : MonoBehaviour
{
    [SerializeField] float delayJump;
    [SerializeField] float delayBlink;
    [SerializeField] BoxCollider2D singoCollider;
    [SerializeField] Signal singoJumpSignal;
    [SerializeField] Signal singoFallSignal;
    private GameObject player;
    private float timer;
    private float timerBlink;
    private bool isJump;
    private Vector3 positionWhenJump;
    private SpriteRenderer sprite;
    private bool isWillJump;
    private bool isSolid;
    private bool isCount;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isWillJump && timerBlink < delayBlink)
        {
            if (isSolid && Mathf.Round(timerBlink * 10.0f) * 0.1f % .4f != 0f)
            {
                Color tmp = sprite.color;
                tmp.a = 1f;
                sprite.color = tmp;
                isSolid = false;
            }
            else if (!isSolid && Mathf.Round(timerBlink * 10.0f) * 0.1f % .4f == 0f)
            {
                Color tmp = sprite.color;
                tmp.a = .3f;
                sprite.color = tmp;
                isSolid = true;
            }
            timerBlink += Time.deltaTime;
        }
        else
        {
            Color tmp = sprite.color;
            tmp.a = 1f;
            sprite.color = tmp;
            isWillJump = false;
            isSolid = false;
            timerBlink = 0f;
        }

        if (isJump && transform.position.y <= positionWhenJump.y)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            singoCollider.isTrigger = false;
            singoFallSignal.Raise();
            positionWhenJump = Vector3.zero;
            isJump = false;
            StartCoroutine(SingoIdleDelay());
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= 3 && !isWillJump && !isCount)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= delayJump)
        {
            StartCoroutine(SingoJumpCo());
            timer = 0;
        }
    }

    private IEnumerator SingoJumpCo()
    {
        gameObject.GetComponent<SingoBarong>().currentState = EnemyState.stat;
        gameObject.GetComponent<EnemyWalk>().anim.SetBool("isWalking", false);
        isWillJump = true;
        yield return new WaitForSeconds(delayBlink);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
        positionWhenJump = transform.position;
        singoCollider.isTrigger = true;
        singoJumpSignal.Raise();
        yield return new WaitForSeconds(0.5f);
        isCount = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().gravityScale = 2;
        isJump = true;
    }

    private IEnumerator SingoIdleDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SingoBarong>().currentState = EnemyState.idle;
        isCount = false;
    }
}
