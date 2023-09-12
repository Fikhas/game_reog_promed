using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Signal enemySignal;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] enemyPos;
    public int enemyAmount;
    private bool isSpawn;
    private bool isSpawned;

    private void Update()
    {
        if (isSpawn)
        {
            for (int i = 0; i < enemyAmount; i++)
            {
                GameObject newEnemy = Instantiate(enemy, enemyPos[i].position, Quaternion.identity);
                newEnemy.GetComponent<Enemy>().deathSignal = enemySignal;
                if (i < enemyAmount)
                {
                    isSpawn = false;
                    isSpawned = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (other.CompareTag("Player"))
            {
                if (gameObject.GetComponent<CutSceneStage>() == null)
                {
                    isSpawn = true;
                }
            }
        }
    }

    public void ManualSpawnEnemy()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject newEnemy = Instantiate(enemy, enemyPos[i].position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().deathSignal = enemySignal;
            if (i < enemyAmount)
            {
                isSpawn = false;
                isSpawned = true;
            }
        }
    }

    public void ChangeSpawnState()
    {
        if (!isSpawned)
        {
            isSpawn = true;
        }
    }
}
