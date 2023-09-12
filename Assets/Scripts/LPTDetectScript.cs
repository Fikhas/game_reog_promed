using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPTDetectScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider2D[] otherTrigger = other.GetComponentsInChildren<BoxCollider2D>();
            otherTrigger[1].isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider2D[] otherTrigger = other.GetComponentsInChildren<BoxCollider2D>();
            otherTrigger[1].isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider2D[] otherTrigger = other.GetComponentsInChildren<BoxCollider2D>();
            otherTrigger[1].isTrigger = false;
        }
    }
}
