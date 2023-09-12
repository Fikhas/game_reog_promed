using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelParentScript : MonoBehaviour
{
    [HideInInspector] public bool isCountToDestroy;
    private float timer;

    private void Update()
    {
        if (isCountToDestroy)
        {
            timer += Time.deltaTime;
        }
        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
