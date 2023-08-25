using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.sharedInstance.healthBar.AddHealth(40);
            other.GetComponent<PlayerMovement>().currentHealth.runtimeValue += 40;
            gameObject.SetActive(false);
        }
    }
}
