using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagement : MonoBehaviour
{
    // [HideInInspector]
    public bool isEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEntered)
        {
            StartCoroutine(EnteredCo());
        }
    }

    private IEnumerator EnteredCo()
    {
        yield return new WaitForSeconds(1f);
        isEntered = true;
    }
}
