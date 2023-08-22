using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    // public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        homePosition = transform;
    }

    public override void CheckDistance()
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
            // if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            // {
            //     Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
            //     ChangeAnimate(temp - transform.position);
            //     myRigidBody.MovePosition(temp);
            // }
            // else
            // {
            //     ChangeGoal();
            // }
            {
                // anim.SetBool("wakeUp", false);
                myRigidBody.MovePosition(homePosition.position);
                anim.SetBool("isWalk", false);
            }
        }
    }

    // private void ChangeGoal()
    // {
    //     if (currentPoint == path.Length - 1)
    //     {
    //         currentPoint = 0;
    //         currentGoal = path[0];
    //     }
    //     else
    //     {
    //         currentPoint++;
    //         currentGoal = path[currentPoint];
    //     }
    // }
}
