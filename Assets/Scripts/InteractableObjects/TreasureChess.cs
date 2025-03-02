using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreasureChess : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] Item item;
	[SerializeField] GameObject placeItem;
	[SerializeField] Inventory playerInventory;
	[SerializeField] Signal signalToRaise;
	[SerializeField] GameObject buttonClue;
	public static TreasureChess sharedInstance;
	private bool isOpened;
	[SerializeField] bool isCanOpen;
	private bool isOnArea;

	void Start()
	{
		sharedInstance = this;
		placeItem.SetActive(false);
		buttonClue.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isCanOpen && isOnArea && Player.sharedInstance.playerCurrentState == PlayerState.interact)
		{
			Player.sharedInstance.animator.SetBool("isHoldItem", false);
			gameObject.GetComponentInChildren<DialogPopUp>().PopUpInactive();
			placeItem.SetActive(false);
			buttonClue.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.Space) && isOnArea && isCanOpen && !isOpened)
		{
			if (signalToRaise != null)
			{
				signalToRaise.Raise();
			}
			anim.SetBool("isOpen", true);
			gameObject.GetComponentInChildren<DialogPopUp>().PopUpActive(item.itemDescription);
			Player.sharedInstance.animator.SetBool("isHoldItem", true);
			placeItem.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
			placeItem.SetActive(true);
			playerInventory.AddItem(item);
			isOpened = true;
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isOnArea = true;
			if (!isOpened && isCanOpen)
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

	public void ChangeOpenState()
	{
		isCanOpen = true;
	}
}
