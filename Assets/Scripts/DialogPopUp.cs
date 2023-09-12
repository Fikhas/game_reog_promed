using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text textDialog;
    [SerializeField] GameObject dialogBox;
    [SerializeField] Signal dialogSignalOff;
    [SerializeField] Signal dialogSignalOn;
    [SerializeField] GameObject playerToFreeze;
    public static DialogPopUp sharedInstance;
    private bool isRaiseOffSignal;

    private void Start()
    {
        sharedInstance = this;
    }

    private void Update()
    {
        if (!dialogBox.activeInHierarchy && isRaiseOffSignal)
        {
            dialogSignalOff.Raise();
            isRaiseOffSignal = false;
        }
    }

    public void PopUpActive(string text)
    {
        // playerToFreeze.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
        textDialog.text = text;
        dialogBox.SetActive(true);
        dialogSignalOn.Raise();
    }

    public void PopUpInactive()
    {
        // playerToFreeze.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.idle;
        dialogBox.SetActive(false);
        isRaiseOffSignal = true;
    }

    public void PopUpActiveWithTime(string text, float popUpTime)
    {
        // playerToFreeze.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
        textDialog.text = text;
        dialogBox.SetActive(true);
        dialogSignalOn.Raise();
        StartCoroutine(PopUpCo(popUpTime));
    }

    private IEnumerator PopUpCo(float popUpTime)
    {
        yield return new WaitForSeconds(popUpTime);
        dialogBox.SetActive(false);
        isRaiseOffSignal = true;
        // playerToFreeze.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.idle;
    }
}
