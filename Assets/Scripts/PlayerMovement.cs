using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    attack,
    walk,
    stagger,
    hit
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
    public HealthBar healthBar;
    [SerializeField] SpriteRenderer sprite;
    public bool isHit;
    private float timer;
    private float delay = 3f;
    private string color = "red";
    public SpawnPoint initSpawnCordinat;
    public GameObject playerDeathPanel;
    // public VectorValue startingPosition;
    // public Inventory playerInventory;
    // public SpriteRenderer receivedItemSprite;

    void Start()
    {
        playerCurrentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        healthBar.SetMaxValue(currentHealth.runtimeValue);
        transform.position = initSpawnCordinat.runtimeSpawnCordinat;
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
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
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
}