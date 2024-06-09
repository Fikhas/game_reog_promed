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

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float speed;
    [SerializeField] GameObject attackSoundEffect;
    [SerializeField] GameObject deathSoundEffect;
    [SerializeField] GameObject knockSoundEffect;
    [SerializeField] GameObject knockWithShieldSoundEffect;
    [SerializeField] GameObject klonoLaughSoundEffect;
    [SerializeField] Animator healthWarning;
    [SerializeField] Signal healthUnder;
    [HideInInspector] public PlayerState playerCurrentState;
    [HideInInspector] public Vector3 change;
    [HideInInspector] public Rigidbody2D myRigidBody;
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isCanAttack;
    [HideInInspector] public float timer;
    public GameObject imunAnim;
    public Animator animator;
    public FloatValue currentHealth;
    public HealthBar healthBar;
    public GameObject playerDeathPanel;
    public static PlayerMovement sharedInstance;
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
        healthWarning.SetBool("isWarningOn", false);
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
        if (currentHealth.runtimeValue >= 30)
        {
            healthWarning.SetBool("isWarningOn", false);
        }
        else
        {
            healthWarning.SetBool("isWarningOn", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("EnemySword"))
        {
            if (playerCurrentState == PlayerState.imun)
            {
                Instantiate(knockWithShieldSoundEffect);
            }
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
            Instantiate(attackSoundEffect);
            isCanAttack = true;
            animator.SetBool("isAttack", true);
            yield return new WaitForSeconds(.3f);
            isCanAttack = false;
            animator.SetBool("isAttack", false);
        }
        else
        {
            Instantiate(attackSoundEffect);
            isCanAttack = true;
            animator.SetBool("isAttack", true);
            playerCurrentState = PlayerState.attack;
            yield return new WaitForSeconds(.3f);
            isCanAttack = false;
            animator.SetBool("isAttack", false);
            if (playerCurrentState != PlayerState.interact)
            {
                playerCurrentState = PlayerState.walk;
            }
        }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        if (currentHealth.runtimeValue > 0)
        {
            Instantiate(knockSoundEffect);
            StartCoroutine(KnockCo(.4f));
            healthBar.SetHealth(currentHealth.runtimeValue);
            healthWarning.SetBool("isWarningOn", false);
            if (currentHealth.runtimeValue < 30)
            {
                healthUnder.Raise();
            }
        }
        else
        {
            Instantiate(deathSoundEffect);
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
        Time.timeScale = 1;
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

    public void KlonoLaugh()
    {
        Instantiate(klonoLaughSoundEffect);
    }
}
