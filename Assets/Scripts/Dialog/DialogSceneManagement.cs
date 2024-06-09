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
    [SerializeField] Signal[] additionalSignalWhenStart;
    [SerializeField] Signal[] additionalSignalWhenEnd;
    [SerializeField] float delayPlay;
    [SerializeField] bool isPlayWithTrigger;
    private bool isRaiseOffSignal;
    private bool isPlayed;
    private float timer;

    private void Update()
    {
        if (!dialogScene.activeInHierarchy && isRaiseOffSignal)
        {
            dialogSceneOffSignal.Raise();
            dialogSceneCamera.SetActive(false);
            mainCamera.SetActive(true);
            canvasToOff.SetActive(true);
            isRaiseOffSignal = false;
            if (additionalSignalWhenEnd != null)
            {
                foreach (Signal signal in additionalSignalWhenEnd)
                {
                    signal.Raise();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayWithTrigger && !isPlayed)
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
                    isPlayed = true;
                    if (additionalSignalWhenStart != null)
                    {
                        foreach (Signal signal in additionalSignalWhenStart)
                        {
                            signal.Raise();
                        }
                    }
                }
            }
        }
    }

    public void PlayDialogScene()
    {
        if (gameObject.GetComponent<StageManagement>().isAnyPlayer && !isPlayed)
        {
            dialogSceneOnSignal.Raise();
            dialogScene.SetActive(true);
            dialogSceneCamera.SetActive(true);
            mainCamera.SetActive(false);
            canvasToOff.SetActive(false);
            isRaiseOffSignal = true;
            isPlayed = true;
            if (additionalSignalWhenStart != null)
            {
                foreach (Signal signal in additionalSignalWhenStart)
                {
                    signal.Raise();
                }
            }
        }
    }

    private IEnumerator PlayDialogSceneCo()
    {
        yield return new WaitForSeconds(delayPlay);
    }
}
