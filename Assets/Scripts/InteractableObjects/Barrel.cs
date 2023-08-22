using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject heart;
    public GameObject smoke;
    private float delay = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            smoke.SetActive(true);
            StartCoroutine(HeartCo());
        }
    }

    private IEnumerator HeartCo()
    {
        yield return new WaitForSeconds(delay);
        heart.SetActive(true);
        smoke.SetActive(false);
        gameObject.SetActive(false);
    }
}
