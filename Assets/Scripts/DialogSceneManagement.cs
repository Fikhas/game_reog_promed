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
    [SerializeField] bool isPlayWithTrigger;
    private bool isRaiseOffSignal;
    private bool isCount;
    private float timer;

    private void Update()
    {
        if (isCount)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.3f)
        {
            dialogScene.SetActive(true);
            dialogSceneCamera.SetActive(true);
            mainCamera.SetActive(false);
            canvasToOff.SetActive(false);
            dialogSceneOnSignal.Raise();
            isRaiseOffSignal = true;
            isCount = false;
        }
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
        if (isPlayWithTrigger)
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

    public void PlayDialogScene()
    {
        // if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
        // {
        //     isCount = true;
        // }
        if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
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
