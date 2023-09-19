using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] GameObject fadeInFadeOut;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject fadeTransition = Instantiate(fadeInFadeOut, Vector3.zero, Quaternion.identity);
            fadeTransition.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }

    public void FadeInFadeOut()
    {
        GameObject fadeTransition = Instantiate(fadeInFadeOut, Vector3.zero, Quaternion.identity);
        fadeTransition.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
