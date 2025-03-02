using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawnDelay : MonoBehaviour
{
    [SerializeField] GameObject cutSceneToManage;
    [SerializeField] Signal enemySpawnSignal;
    private bool isRaiseOffSignal;

    private void Update()
    {
        if (!cutSceneToManage.activeInHierarchy && isRaiseOffSignal)
        {
            enemySpawnSignal.Raise();
            isRaiseOffSignal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(EnemyDelayCo());
            }
        }
    }

    private IEnumerator EnemyDelayCo()
    {
        yield return new WaitForSeconds(1.4f);
        isRaiseOffSignal = true;
    }
}
