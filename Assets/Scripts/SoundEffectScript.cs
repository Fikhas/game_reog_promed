using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
