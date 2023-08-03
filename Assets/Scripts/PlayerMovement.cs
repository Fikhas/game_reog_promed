using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    attack,
    walk,
    stagger
}
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    [SerializeField] Animator animator;
    public PlayerState playerCurrentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    // public VectorValue startingPosition;
    // public Inventory playerInventory;
    // public SpriteRenderer receivedItemSprite;

    void Start()
    {
        playerCurrentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }


    void Update()
    {
        change = Vector3.zero;
        if (playerCurrentState != PlayerState.attack)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetButtonDown("Attack"))
        {
            StartCoroutine(attackCo());
        }
        else
        {
            AnimationUpdate();
        }
    }

    void AnimationUpdate()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isWalk", true);
            // MoveCharacter();
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    void FixedUpdate()
    {
        change.Normalize();
        myRigidBody.velocity = new Vector3(change.x * speed, change.y * speed, change.z);
    }

    // void MoveCharacter()
    // {
    //     myRigidBody.MovePosition(
    //         transform.position + change * speed * Time.deltaTime
    //     );
    // }

    private IEnumerator attackCo()
    {
        animator.SetBool("isAttack", true);
        playerCurrentState = PlayerState.attack;
        // yield return null;
        yield return new WaitForSeconds(.3f);
        animator.SetBool("isAttack", false);
        playerCurrentState = PlayerState.walk;
        // if (currentState != PlayerState.interact)
        // {
        //     currentState = PlayerState.walk;
        // }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        myRigidBody.velocity = Vector2.zero;
        playerCurrentState = PlayerState.idle;
    }
}
