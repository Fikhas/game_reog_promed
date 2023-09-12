using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSingoActive : MonoBehaviour
{
    [SerializeField] GameObject healthBar;

    private void Start()
    {
        healthBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthBar.SetActive(false);
        }
    }
}
