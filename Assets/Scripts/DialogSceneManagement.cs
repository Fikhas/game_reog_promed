using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSceneManagement : MonoBehaviour
{
    [SerializeField] GameObject dialogScene;
    [SerializeField] GameObject dialogSceneCamera;
    [SerializeField] GameObject mainCamera;
    [SerializeField] Signal dialogSceneOffSignal;
    [SerializeField] Signal dialogSceneOnSignal;
    [SerializeField] GameObject canvasToOff;
    private bool isRaiseOffSignal;

    private void Update()
    {
        if (!dialogScene.activeInHierarchy && isRaiseOffSignal)
        {
            dialogSceneOffSignal.Raise();
            dialogSceneCamera.SetActive(false);
            mainCamera.SetActive(true);
            canvasToOff.SetActive(true);
            isRaiseOffSignal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (other.CompareTag("Player"))
            {
                dialogScene.SetActive(true);
                dialogSceneCamera.SetActive(true);
                mainCamera.SetActive(false);
                canvasToOff.SetActive(false);
                dialogSceneOnSignal.Raise();
                isRaiseOffSignal = true;
            }
        }
    }
}
