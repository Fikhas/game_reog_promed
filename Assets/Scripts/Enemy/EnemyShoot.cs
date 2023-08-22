using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Enemy
{
    private Rigidbody2D myRigidBody;
    private GameObject target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    private void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        homePosition = gameObject.transform;
        anim.SetBool("isWalk", false);
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius && Vector3.Distance(target.transform.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                anim.SetBool("isWalk", true);
                ChangeAnimate(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                // anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius)
        {
            // anim.SetBool("wakeUp", false);
            myRigidBody.MovePosition(homePosition.position);
            anim.SetBool("isWalk", false);
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
        if (newState != currentState)
        {
            currentState = newState;
        }
    }
}
