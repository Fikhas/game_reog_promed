using System.Collections;
using System.Collections.Generic;
using Fikhas.Audio;
using Unity.VisualScripting;
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

public class Player : MonoBehaviour
{
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] Animator healthWarning;
	[SerializeField] Signal healthUnder;
	public PlayerState playerCurrentState;
	[HideInInspector] public Vector2 change;
	[HideInInspector] public Rigidbody2D myRigidBody;
	[HideInInspector] public bool isHit;
	[HideInInspector] public bool isCanAttack;
	[HideInInspector] public float timer;
	public GameObject imunAnim;
	public Animator animator;
	public FloatValue currentHealth;
	public HealthBar healthBar;
	public GameObject playerDeathPanel;
	public static Player sharedInstance;
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
			attackCo = StartCoroutine(AttackCo());
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

	void AnimationUpdate()
	{
		if (change != Vector2.zero)
		{
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("isWalk", true);
		}
		else
		{
			animator.SetBool("isWalk", false);
		}
	}

	public void Knock(float knockTime, float damage)
	{
		currentHealth.runtimeValue -= damage;
		if (currentHealth.runtimeValue > 0)
		{
			SoundSystem.Instance.PlayAudio("KlonoKnock", false, "k-knock");
			knockCo = StartCoroutine(KnockCo(.4f));
			healthBar.SetHealth(currentHealth.runtimeValue);
			healthWarning.SetBool("isWarningOn", false);
			if (currentHealth.runtimeValue < 30)
			{
				healthUnder.Raise();
			}
		}
		else
		{
			SoundSystem.Instance.PlayAudio("KlonoDeath", false, "k-death");
			this.gameObject.SetActive(false);
			healthBar.SetHealth(currentHealth.runtimeValue);
			playerDeathPanel.SetActive(true);
			Time.timeScale = 0;
		}
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
		imunActiveCo = StartCoroutine(ImunActiveCo());
	}

	public void KlonoLaugh()
	{
		SoundSystem.Instance.PlayAudio("KlonoLaugh", false, "k-laugh");
	}

	Coroutine imunActiveCo;
	private IEnumerator ImunActiveCo()
	{
		if (imunActiveCo != null)
		{
			StopCoroutine(imunActiveCo);
			imunActiveCo = null;
		}
		Instantiate(imunAnim, this.transform, worldPositionStays: false);
		yield return new WaitForSeconds(3f);
		Destroy(GameObject.FindGameObjectWithTag("ImunAnim"));
	}

	Coroutine knockCo;
	private IEnumerator KnockCo(float knockTime)
	{
		if (knockCo != null)
		{
			StopCoroutine(knockCo);
			knockCo = null;
		}

		yield return new WaitForSeconds(knockTime);
		myRigidBody.velocity = Vector2.zero;
		playerCurrentState = PlayerState.idle;
	}

	Coroutine attackCo;
	private IEnumerator AttackCo()
	{
		if (attackCo != null)
		{
			StopCoroutine(attackCo);
			attackCo = null;
		}

		if (playerCurrentState == PlayerState.imun)
		{
			SoundSystem.Instance.PlayAudio("KlonoAttack", false, "k-attack");
			isCanAttack = true;
			animator.SetBool("isAttack", true);
			yield return new WaitForSeconds(.3f);
			isCanAttack = false;
			animator.SetBool("isAttack", false);
		}
		else
		{
			SoundSystem.Instance.PlayAudio("KlonoAttack", false, "k-attack");
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Bullet") || other.CompareTag("EnemySword"))
		{
			if (playerCurrentState == PlayerState.imun)
			{
				SoundSystem.Instance.PlayAudio("KlonoShield", false, "k-shield");
			}
		}
	}

	// FOR MOBILE
	public void PlayerAttack()
	{
		if (!isCanAttack && playerCurrentState != PlayerState.interact)
		{
			attackCo = StartCoroutine(AttackCo());
		}
		else
		{
			PlayerMovementMobile.Instance.AnimationUpdate();
		}
	}
}
