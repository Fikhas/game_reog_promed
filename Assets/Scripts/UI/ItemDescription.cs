using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
	[SerializeField] private float waitingTime;
	[SerializeField] private float animTime;
	[SerializeField] private GameObject skipButton;
	[SerializeField] private Animator anim;
	[SerializeField] private Signal dialogSignalOn;
	[SerializeField] private Signal dialogSignalOff;

	private void OnEnable()
	{
		dialogSignalOn.Raise();
		itemDescriptionCo = StartCoroutine(ItemDescriptionCo());
	}

	private void OnDisable()
	{
		if (itemDescriptionCo != null)
		{
			StopCoroutine(itemDescriptionCo);
			itemDescriptionCo = null;
		}

		if (skipButtonCo != null)
		{
			StopCoroutine(skipButtonCo);
			skipButtonCo = null;
		}

		anim.SetBool("isSkip", false);
	}

	private Coroutine itemDescriptionCo;
	private IEnumerator ItemDescriptionCo()
	{
		yield return new WaitForSeconds(waitingTime);
		skipButton.SetActive(true);
	}

	public void SkipButtonAction()
	{
		skipButtonCo = StartCoroutine(SkipButtonCo());
	}

	private Coroutine skipButtonCo;
	private IEnumerator SkipButtonCo()
	{
		anim.SetBool("isSkip", true);
		yield return new WaitForSeconds(animTime);
		dialogSignalOff.Raise();

		if (GameObject.FindGameObjectWithTag("PlaceItem") != null)
		{
			GameObject.FindGameObjectWithTag("PlaceItem").gameObject.SetActive(false);
		}

		Player.sharedInstance.animator.SetBool("isHoldItem", false);
		gameObject.SetActive(false);
	}
}
