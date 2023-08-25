using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeContinue : MonoBehaviour
{
    private GameObject panelMainMenu;

    private void Start()
    {
        panelMainMenu = GameObject.FindGameObjectWithTag("PanelMainMenu");
        panelMainMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelMainMenu.SetActive(true);
        }
    }
}
