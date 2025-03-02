using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyCo());
    }

    private IEnumerator DestroyCo()
    {
        yield return new WaitForSeconds(5.1f);
        Destroy(gameObject);
    }
}
