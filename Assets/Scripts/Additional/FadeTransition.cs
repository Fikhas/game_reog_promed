using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] GameObject fadeInFadeOut;
    [SerializeField] bool isPlayWithSignal;
    [SerializeField] bool isPlayWithStart;

    private void Start()
    {
        if (isPlayWithStart)
        {
            GameObject fadeTransition = Instantiate(fadeInFadeOut, Vector3.zero, Quaternion.identity);
            fadeTransition.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPlayWithSignal)
        {
            if (other.CompareTag("Player"))
            {
                GameObject fadeTransition = Instantiate(fadeInFadeOut, Vector3.zero, Quaternion.identity);
                fadeTransition.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
        }
    }

    public void FadeTransitionActive()
    {
        if (isPlayWithSignal)
        {
            GameObject fadeTransition = Instantiate(fadeInFadeOut, Vector3.zero, Quaternion.identity);
            fadeTransition.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }
}
