using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance;

	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private float moveSpeed;

	private Vector2 change;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		change = Vector2.zero;

		if (Player.sharedInstance.playerCurrentState != PlayerState.attack && Player.sharedInstance.playerCurrentState != PlayerState.stagger && Player.sharedInstance.playerCurrentState != PlayerState.interact)
		{
			change.x = Input.GetAxisRaw("Horizontal");
			change.y = Input.GetAxisRaw("Vertical");
		}

		if (Input.GetButtonDown("Attack") && !Player.sharedInstance.isCanAttack && Player.sharedInstance.playerCurrentState != PlayerState.interact)
		{
			Player.sharedInstance.attackCo = StartCoroutine(Player.sharedInstance.AttackCo());
		}
		else
		{
			AnimationUpdate();
		}
	}

	void FixedUpdate()
	{
		if (Player.sharedInstance.playerCurrentState != PlayerState.stagger)
		{
			change.Normalize();
			rb.velocity = new Vector3(change.x * moveSpeed, change.y * moveSpeed);
		}
	}

	public void AnimationUpdate()
	{
		if (change != Vector2.zero)
		{
			Player.sharedInstance.animator.SetFloat("moveX", change.x);
			Player.sharedInstance.animator.SetFloat("moveY", change.y);
			Player.sharedInstance.animator.SetBool("isWalk", true);
		}
		else
		{
			Player.sharedInstance.animator.SetBool("isWalk", false);
		}
	}
}
