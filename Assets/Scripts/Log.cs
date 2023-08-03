using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        // anim.SetBool("wakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimate(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
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
