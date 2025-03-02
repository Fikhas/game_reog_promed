using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    [SerializeField] GameObject[] uiToManage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (gameObject.GetComponent<DialogSceneManagement>() != null)
            {
                if (other.CompareTag("Player"))
                {
                    foreach (GameObject ui in uiToManage)
                    {
                        ui.SetActive(false);
                    }
                }
            }
        }
    }

    public void ActivateUI()
    {
        foreach (GameObject ui in uiToManage)
        {
            ui.SetActive(true);
        }
    }
}
