using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialog : MonoBehaviour
{
    public GameObject dialogToActive;
    [SerializeField] Signal dialogSignalOff;
    [SerializeField] Signal dialogSignalOn;
    private bool isRaiseOffSignal;

    private void Start()
    {
        dialogSignalOn.Raise();
        dialogToActive.SetActive(true);
        isRaiseOffSignal = true;
    }

    private void Update()
    {
        if (!dialogToActive.activeInHierarchy && isRaiseOffSignal)
        {
            dialogSignalOff.Raise();
            isRaiseOffSignal = false;
            Destroy(gameObject);
        }
    }
}
