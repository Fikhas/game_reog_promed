using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryGame : MonoBehaviour
{
    public static RetryGame sharedInstance;
    [SerializeField] GameObject player;
    [HideInInspector]
    public GameObject currentStage;

    private void Start()
    {
        sharedInstance = this;
    }

    public void RetryGameMethod()
    {
        EnemySpawn isThereEnemy = currentStage.GetComponent<EnemySpawn>();
        if (isThereEnemy)
        {
            GameObject[] enemyInActive = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemyInActive.Length; i++)
            {
                Destroy(enemyInActive[i]);
            }
            isThereEnemy.ManualSpawnEnemy();
        }
        PlayerMovement.sharedInstance.transform.position = currentStage.GetComponent<StageManagement>().spawnPosition;
        PlayerMovement.sharedInstance.currentHealth.runtimeValue = 10;
        PlayerMovement.sharedInstance.animator.SetFloat("moveX", 0);
        PlayerMovement.sharedInstance.animator.SetFloat("moveY", -1);
        PlayerMovement.sharedInstance.healthBar.SetMaxValue(PlayerMovement.sharedInstance.currentHealth.runtimeValue);
        PlayerMovement.sharedInstance.healthBar.SetHealth(PlayerMovement.sharedInstance.currentHealth.runtimeValue);
        PlayerMovement.sharedInstance.playerDeathPanel.SetActive(false);
        PlayerMovement.sharedInstance.playerCurrentState = PlayerState.idle;
        PlayerMovement.sharedInstance.isHit = false;
        PlayerMovement.sharedInstance.myRigidBody = player.GetComponent<Rigidbody2D>();
        PlayerMovement.sharedInstance.timer = 0;
        Time.timeScale = 1;
        player.SetActive(true);
    }
}
