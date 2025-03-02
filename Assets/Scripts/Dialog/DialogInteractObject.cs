using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteractObject : MonoBehaviour
{
	[SerializeField] GameObject dialogToActive;
	[SerializeField] Signal dialogSignalOff;
	[SerializeField] Signal dialogSignalOn;
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
			Player.sharedInstance.animator.SetBool("isHoldItem", false);
		}
	}

	public void PopUpActive()
	{
		dialogSignalOn.Raise();
		dialogToActive.SetActive(true);
		isRaiseOffSignal = true;
	}
}
