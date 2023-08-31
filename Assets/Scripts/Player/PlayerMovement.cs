using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    attack,
    walk,
    stagger,
    hit,
    imun,
    interact
}

public enum PlayerFacing
{
    right,
    left,
    up,
    down
}
public class PlayerMovement : MonoBehaviour
{
    public Vector3 change;
    public Rigidbody2D myRigidBody;
    public Animator animator;
    public PlayerState playerCurrentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public HealthBar healthBar;
    [SerializeField] SpriteRenderer sprite;
    public SpawnPoint initSpawnCordinat;
    public GameObject playerDeathPanel;
    public static PlayerMovement sharedInstance;
    // public PlayerFacing playerFacing;
    public Vector2 playerFacing;
    public float speed;
    public bool isHit;
    public float timer;
    private float delay = 3f;
    private string color = "red";
    private bool isCanAttack;

    void Start()
    {
        sharedInstance = this;
        playerCurrentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        healthBar.SetMaxValue(currentHealth.runtimeValue);
        healthBar.SetHealth(currentHealth.runtimeValue);
        // playerFacing = PlayerFacing.down;
    }


    void Update()
    {
        change = Vector3.zero;
        if (playerCurrentState != PlayerState.attack && playerCurrentState != PlayerState.stagger && playerCurrentState != PlayerState.interact)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            playerFacing = new Vector2(change.x, change.y);
            // if (change.x > 0)
            // {
            //     playerFacing = PlayerFacing.right;
            // }
            // else if (change.x < 0)
            // {
            //     playerFacing = PlayerFacing.left;
            // }
            // else if (change.y > 0)
            // {
            //     playerFacing = PlayerFacing.up;
            // }
            // else
            // {
            //     playerFacing = PlayerFacing.down;
            // }
        }
        if (Input.GetButtonDown("Attack") && !isCanAttack)
        {
            StartCoroutine(AttackCo());
        }
        else
        {
            AnimationUpdate();
        }
        if (isHit && timer < delay)
        {
            // playerCurrentState = PlayerState.hit;
            if (color == "red" && Mathf.Round(timer * 10.0f) * 0.1f % .4f != 0f)
            {
                sprite.material.SetColor("_Color", Color.red);
                color = "white";
            }
            else if (color == "white" && Mathf.Round(timer * 10.0f) * 0.1f % .4f == 0f)
            {
                sprite.material.SetColor("_Color", Color.white);
                color = "red";
            }
            timer += Time.deltaTime;
        }
        else
        {
            isHit = false;
            timer = 0f;
            sprite.material.SetColor("_Color", Color.white);
            // playerCurrentState = PlayerState.walk;
        }
        if (currentHealth.runtimeValue >= 100)
        {
            currentHealth.runtimeValue -= 1;
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
        if (playerCurrentState != PlayerState.stagger)
        {
            change.Normalize();
            myRigidBody.velocity = new Vector3(change.x * speed, change.y * speed, change.z);
        }
    }

    // void MoveCharacter()
    // {
    //     myRigidBody.MovePosition(
    //         transform.position + change * speed * Time.deltaTime
    //     );
    // }

    private IEnumerator AttackCo()
    {
        if (playerCurrentState == PlayerState.imun)
        {
            isCanAttack = true;
            animator.SetBool("isAttack", true);
            yield return new WaitForSeconds(.3f);
            isCanAttack = false;
            animator.SetBool("isAttack", false);
        }
        else
        {
            isCanAttack = true;
            animator.SetBool("isAttack", true);
            playerCurrentState = PlayerState.attack;
            // yield return null;
            yield return new WaitForSeconds(.3f);
            isCanAttack = false;
            animator.SetBool("isAttack", false);
            playerCurrentState = PlayerState.walk;
            // currentState = PlayerState.walk;
        }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        // playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCo(.4f));
            healthBar.SetHealth(currentHealth.runtimeValue);
            // sprite.material.SetColor("_Color", Color.red);
        }
        else
        {
            this.gameObject.SetActive(false);
            healthBar.SetHealth(currentHealth.runtimeValue);
            playerDeathPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        myRigidBody.velocity = Vector2.zero;
        playerCurrentState = PlayerState.idle;
        // sprite.material.SetColor("_Color", Color.white);
    }

    public void SetPlayerSpawn()
    {
        playerDeathPanel.SetActive(false);
        Time.timeScale = 1;
        playerCurrentState = PlayerState.idle;
        sprite.material.SetColor("_Color", Color.white);
        gameObject.SetActive(true);
        currentHealth.runtimeValue = currentHealth.initialValue;
        healthBar.SetMaxValue(currentHealth.runtimeValue);
        gameObject.transform.position = initSpawnCordinat.runtimeSpawnCordinat;
        timer = 0;
        isHit = false;
    }

    public void SetPlayerToStagger()
    {
        playerCurrentState = PlayerState.stagger;
    }

    public void SetPlayerToInteract()
    {
        playerCurrentState = PlayerState.interact;
    }

    public void SetPlayerToIdle()
    {
        playerCurrentState = PlayerState.idle;
    }
}
