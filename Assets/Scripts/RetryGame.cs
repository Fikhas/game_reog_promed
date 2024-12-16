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
        PlayerMovement.sharedInstance.transform.position = currentStage.GetComponent<StageManagement>().spawnPosition;
        PlayerMovement.sharedInstance.currentHealth.runtimeValue = PlayerMovement.sharedInstance.currentHealth.initialValue;
        PlayerMovement.sharedInstance.animator.SetFloat("moveX", 0);
        PlayerMovement.sharedInstance.animator.SetFloat("moveY", -1);
        PlayerMovement.sharedInstance.healthBar.SetMaxValue(PlayerMovement.sharedInstance.currentHealth.runtimeValue);
        PlayerMovement.sharedInstance.healthBar.SetHealth(PlayerMovement.sharedInstance.currentHealth.runtimeValue);
        PlayerMovement.sharedInstance.playerDeathPanel.SetActive(false);
        PlayerMovement.sharedInstance.playerCurrentState = PlayerState.idle;
        PlayerMovement.sharedInstance.isCanAttack = false;
        PlayerMovement.sharedInstance.isHit = false;
        PlayerMovement.sharedInstance.myRigidBody = player.GetComponent<Rigidbody2D>();
        PlayerMovement.sharedInstance.timer = 0;
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
