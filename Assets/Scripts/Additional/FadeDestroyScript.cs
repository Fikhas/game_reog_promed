using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeDestroyScript : MonoBehaviour
{
    public void DestroyFade()
    {
        StartCoroutine(DestroyFadeCo());
    }

    private IEnumerator DestroyFadeCo()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
