using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    [SerializeField] GameObject heartSoundEffect;
    private float timer;
    [SerializeField] bool isWithTime;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10 && isWithTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(heartSoundEffect);
            PlayerMovement.sharedInstance.healthBar.AddHealth(20);
            other.GetComponent<PlayerMovement>().currentHealth.runtimeValue += 20;
            Destroy(gameObject);
        }
    }
}
