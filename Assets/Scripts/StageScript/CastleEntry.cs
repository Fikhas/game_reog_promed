using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntry : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        yield return new WaitForSeconds(1f);
        player.GetComponent<Transform>().position = new Vector3(3, 38, 0);
    }
}
