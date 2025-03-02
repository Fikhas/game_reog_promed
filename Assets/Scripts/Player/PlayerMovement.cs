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

	private void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		if (Player.sharedInstance.playerCurrentState != PlayerState.stagger)
		{
			Player.sharedInstance.change.Normalize();
			rb.velocity = new Vector3(Player.sharedInstance.change.x * moveSpeed, Player.sharedInstance.change.y * moveSpeed);
		}
	}

	void AnimationUpdate()
	{
		if (Player.sharedInstance.change != Vector2.zero)
		{
			Player.sharedInstance.animator.SetFloat("moveX", Player.sharedInstance.change.x);
			Player.sharedInstance.animator.SetFloat("moveY", Player.sharedInstance.change.y);
			Player.sharedInstance.animator.SetBool("isWalk", true);
		}
		else
		{
			Player.sharedInstance.animator.SetBool("isWalk", false);
		}
	}
}
