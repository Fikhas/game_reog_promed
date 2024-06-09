using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeContinue : MonoBehaviour
{
    private GameObject panelMainMenu;
    [SerializeField] bool isInstantiateWithTrigger;

    private void Start()
    {
        panelMainMenu = GameObject.FindGameObjectWithTag("PanelMainMenu");
        panelMainMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isInstantiateWithTrigger)
            {
                StartCoroutine(InstantPanelCo());
            }
        }
    }

    public void InstantPanelMenu()
    {
        if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
        {
            StartCoroutine(InstantPanelCo());
        }
    }

    private IEnumerator InstantPanelCo()
    {
        yield return new WaitForSeconds(1f);
        panelMainMenu.SetActive(true);
    }
}
