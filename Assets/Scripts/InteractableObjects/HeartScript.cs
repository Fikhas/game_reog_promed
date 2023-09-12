using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.sharedInstance.healthBar.AddHealth(20);
            other.GetComponent<PlayerMovement>().currentHealth.runtimeValue += 20;
            Destroy(gameObject);
        }
    }
}
