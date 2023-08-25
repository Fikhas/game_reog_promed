using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : MonoBehaviour
{
    [SerializeField] Signal signalToRaise;
    private int enemyDeathAmount;
    private int enemyAmount;

    public void EnemyCheck()
    {
        enemyAmount = gameObject.GetComponent<EnemySpawn>().enemyAmount;
        enemyDeathAmount += 1;
        if (enemyDeathAmount == enemyAmount)
        {
            signalToRaise.Raise();
        }
    }
}
