using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryGame : MonoBehaviour
{
	public static RetryGame sharedInstance;
	[SerializeField] GameObject player;
	[SerializeField] GameObject barrelStage9;
	[SerializeField] Vector2[] barrelStage9Pos;
	[HideInInspector] public GameObject currentStage;

	private void Start()
	{
		sharedInstance = this;
	}

	public void RetryGameMethod()
	{
		EnemySpawn isThereEnemy = currentStage.GetComponent<EnemySpawn>();
		if (isThereEnemy)
		{
			isThereEnemy.spawnDelayTime = 0;
		}
		currentStage.GetComponent<StageManagement>().isAnyPlayer = true;
		if (GameObject.FindGameObjectsWithTag("Bullet") != null)
		{
			GameObject[] bulletToDestroy = GameObject.FindGameObjectsWithTag("Bullet");
			foreach (GameObject bullet in bulletToDestroy)
			{
				Destroy(bullet);
			}
		}
		if (isThereEnemy)
		{
			isThereEnemy.isSpawned = false;
			GameObject[] enemyInActive = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < enemyInActive.Length; i++)
			{
				Destroy(enemyInActive[i]);
			}
			isThereEnemy.ManualSpawnEnemy();
		}
		Player.sharedInstance.transform.position = currentStage.GetComponent<StageManagement>().spawnPosition;
		Player.sharedInstance.currentHealth.runtimeValue = Player.sharedInstance.currentHealth.initialValue;
		Player.sharedInstance.animator.SetFloat("moveX", 0);
		Player.sharedInstance.animator.SetFloat("moveY", -1);
		Player.sharedInstance.healthBar.SetMaxValue(Player.sharedInstance.currentHealth.runtimeValue);
		Player.sharedInstance.healthBar.SetHealth(Player.sharedInstance.currentHealth.runtimeValue);
		Player.sharedInstance.playerDeathPanel.SetActive(false);
		Player.sharedInstance.playerCurrentState = PlayerState.idle;
		Player.sharedInstance.isCanAttack = false;
		Player.sharedInstance.isHit = false;
		Player.sharedInstance.myRigidBody = player.GetComponent<Rigidbody2D>();
		Player.sharedInstance.timer = 0;
		Time.timeScale = 1;
		player.SetActive(true);
		if (currentStage.CompareTag("Stage9"))
		{
			Destroy(GameObject.FindGameObjectWithTag("BarrelClue"));
			Destroy(GameObject.FindGameObjectWithTag("BarrelDrop"));
			Destroy(GameObject.FindGameObjectWithTag("BarrelStage9"));
			Destroy(GameObject.FindGameObjectWithTag("Heart"));
			foreach (Vector2 barrelPos in barrelStage9Pos)
			{
				Instantiate(barrelStage9, barrelPos, Quaternion.identity);
			}
		}
	}
}
