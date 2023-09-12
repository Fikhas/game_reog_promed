using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntry : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    [SerializeField] GameObject fadeInFadeOut;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = fadeInFadeOut.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CastleInCo());
        }
    }

    private IEnumerator CastleInCo()
    {
        anim.SetBool("fadeIn", true);
        anim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("fadeIn", false);
        anim.SetBool("fadeOut", false);
        player.GetComponent<Transform>().position = new Vector3(3, 38, 0);
        // yield return new WaitForSeconds(1f);
    }
}
