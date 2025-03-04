using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreasureChessWithDialog : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] Item item;
	[SerializeField] GameObject placeItem;
	[SerializeField] Inventory playerInventory;
	[SerializeField] Signal signalToRaise;
	[SerializeField] GameObject buttonClue;
	[SerializeField] GameObject openSoundEffect;
	[SerializeField] GameObject getItemSoundEffect;
	[SerializeField] GameObject itemDesc;
	[SerializeField] bool isCanOpen;
	[SerializeField] bool isAnyItemDesc;

	private bool isOpened;
	private bool isOnArea;

	void Start()
	{
		placeItem.SetActive(false);
		buttonClue.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnArea && isCanOpen && Player.sharedInstance.playerCurrentState == PlayerState.interact)
		{
			Player.sharedInstance.animator.SetBool("isHoldItem", false);
			placeItem.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.Space) && isOnArea && buttonClue.activeInHierarchy)
		{
			if (openChessCo != null)
			{
				StopCoroutine(openChessCo);
				openChessCo = null;
			}

			openChessCo = StartCoroutine(OpenChessCo());
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isOnArea = true;
			if (!isOpened && isCanOpen && isOnArea)
			{
				buttonClue.SetActive(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isOnArea = false;
			buttonClue.SetActive(false);
		}
	}

	Coroutine openChessCo;
	private IEnumerator OpenChessCo()
	{
		yield return new WaitForSeconds(0f);
		anim.SetBool("isOpen", true);
		Instantiate(openSoundEffect);
		Instantiate(getItemSoundEffect);
		playerInventory.AddItem(item);
		buttonClue.SetActive(false);
		Player.sharedInstance.animator.SetBool("isHoldItem", true);
		placeItem.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
		placeItem.SetActive(true);
		isOpened = true;
		signalToRaise.Raise();

		if (isAnyItemDesc)
		{
			if (itemDesc != null)
			{
				itemDesc.SetActive(true);
			}
		}
		else
		{
			gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
		}
	}

	public void ChangeOpenState()
	{
		isCanOpen = true;
	}

	public void InteractItem()
	{
		if (isOnArea && isCanOpen && Player.sharedInstance.playerCurrentState == PlayerState.interact)
		{
			Player.sharedInstance.animator.SetBool("isHoldItem", false);
			placeItem.SetActive(false);
		}
		else if (isOnArea && buttonClue.activeInHierarchy)
		{
			openChessCo = StartCoroutine(OpenChessCo());
		}
	}
}
