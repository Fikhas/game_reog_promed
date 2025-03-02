using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private float smoother;

	private void LateUpdate()
	{
		Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		transform.position = Vector3.Lerp(transform.position, playerPos, smoother);
	}
}
