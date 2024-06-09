using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class StageManagement : MonoBehaviour
{
    public Vector3 spawnPosition;
    [HideInInspector] public bool isEntered;
    [HideInInspector] public bool isAnyPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RetryGame.sharedInstance.currentStage = this.gameObject;
        if (other.CompareTag("Player"))
        {
            if (!isEntered)
            {
                isAnyPlayer = true;
                StartCoroutine(EnteredCo());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAnyPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAnyPlayer = false;
        }
    }

    private IEnumerator EnteredCo()
    {
        yield return new WaitForSeconds(.01f);
        isEntered = true;
    }
}
