using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public GameObject target;
    public Animator anim;
    [SerializeField] float moveSpeed;
    public float chaseRadius;
    public float attackRadius;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
        {
            if (gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                ChangeState(EnemyState.idle);
                anim.SetBool("isWalking", false);
                ChangeAnimate(temp - transform.position);
            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius && Vector3.Distance(target.transform.position, transform.position) > attackRadius)
        {
            if (gameObject.GetComponent<Enemy>().currentState == EnemyState.idle || gameObject.GetComponent<Enemy>().currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                anim.SetBool("isWalking", true);
                myRigidBody.MovePosition(temp);
                ChangeAnimate(temp - transform.position);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius && gameObject.GetComponent<Enemy>().currentState != EnemyState.stagger)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("isWalking", false);
        }
    }

    public void SetChangeAnimate(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    public void ChangeAnimate(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetChangeAnimate(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetChangeAnimate(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetChangeAnimate(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetChangeAnimate(Vector2.down);
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (newState != gameObject.GetComponent<Enemy>().currentState)
        {
            gameObject.GetComponent<Enemy>().currentState = newState;
        }
    }
}
