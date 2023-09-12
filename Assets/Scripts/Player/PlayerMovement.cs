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
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float speed;

    public GameObject imunAnim;
    public Vector3 change;
    public Animator animator;
    public PlayerState playerCurrentState;
    public FloatValue currentHealth;
    public HealthBar healthBar;
    public GameObject playerDeathPanel;
    public static PlayerMovement sharedInstance;

    [HideInInspector] public Rigidbody2D myRigidBody;
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isCanAttack;
    [HideInInspector] public float timer;
    private float delay = 2f;
    private string color = "red";

    void Start()
    {
        sharedInstance = this;
        playerCurrentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        healthBar.SetMaxValue(currentHealth.runtimeValue);
        healthBar.SetHealth(currentHealth.runtimeValue);
    }


    void Update()
    {
        change = Vector3.zero;
        if (playerCurrentState != PlayerState.attack && playerCurrentState != PlayerState.stagger && playerCurrentState != PlayerState.interact)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetButtonDown("Attack") && !isCanAttack && playerCurrentState != PlayerState.interact)
        {
            StartCoroutine(AttackCo());
        }
        else
        {
            AnimationUpdate();
        }
        if (isHit && timer < delay)
        {
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
            yield return new WaitForSeconds(.3f);
            isCanAttack = false;
            animator.SetBool("isAttack", false);
            playerCurrentState = PlayerState.walk;
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

    public void ImunEffectAtive()
    {
        StartCoroutine(ImunActiveCo());
    }

    private IEnumerator ImunActiveCo()
    {
        Instantiate(imunAnim, this.transform, worldPositionStays: false);
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.FindGameObjectWithTag("ImunAnim"));
    }
}
