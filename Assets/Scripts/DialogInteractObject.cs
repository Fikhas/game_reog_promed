using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteractObject : MonoBehaviour
{
    [SerializeField] GameObject dialogToActive;
    [SerializeField] Signal dialogSignalOff;
    [SerializeField] Signal dialogSignalOn;
    [SerializeField] GameObject playerToFreeze;
    [SerializeField] GameObject parentObject;
    private bool isRaiseOffSignal;

    private void Update()
    {
        if (!dialogToActive.activeInHierarchy && isRaiseOffSignal)
        {
            dialogSignalOff.Raise();
            isRaiseOffSignal = false;
            if (parentObject != null)
            {
                parentObject.SetActive(false);
            }
            if (GameObject.FindGameObjectWithTag("PlaceItem") != null)
            {
                GameObject.FindGameObjectWithTag("PlaceItem").gameObject.SetActive(false);
            }
            PlayerMovement.sharedInstance.animator.SetBool("isHoldItem", false);
        }
    }

    public void PopUpActive()
    {
        playerToFreeze.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
        dialogToActive.SetActive(true);
        dialogSignalOn.Raise();
        isRaiseOffSignal = true;
    }
}
