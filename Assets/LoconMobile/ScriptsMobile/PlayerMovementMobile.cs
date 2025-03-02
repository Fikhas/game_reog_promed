using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementMobile : MonoBehaviour
{
	public static PlayerMovementMobile Instance;

	[SerializeField]
	private FixedJoystick joystick;
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private float moveSpeed;

	private Vector2 change;

	private void Awake()
	{
		Instance = this;
	}

	private void FixedUpdate()
	{
		change = Vector2.zero;
		if (Player.sharedInstance.playerCurrentState != PlayerState.attack && Player.sharedInstance.playerCurrentState != PlayerState.interact)
		{
			change = new Vector2(joystick.Horizontal, joystick.Vertical);
			change = change.normalized;
		}
		rb.velocity = new Vector2(change.x * moveSpeed, change.y * moveSpeed);
		AnimationUpdate();
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
