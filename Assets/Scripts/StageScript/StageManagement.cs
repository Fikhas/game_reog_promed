using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class StageManagement : MonoBehaviour
{
    public Vector3 spawnPosition;
    [SerializeField] Signal singoThrowSignal;

    [HideInInspector]
    public bool isEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RetryGame.sharedInstance.currentStage = this.gameObject;
        if (other.CompareTag("Player"))
        {
            if (!isEntered)
            {
                StartCoroutine(EnteredCo());
            }
        }
    }

    private IEnumerator EnteredCo()
    {
        yield return new WaitForSeconds(.01f);
        isEntered = true;
    }
}
