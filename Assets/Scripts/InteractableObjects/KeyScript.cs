using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
	[SerializeField] Signal signalToRaise;
	[SerializeField] GameObject buttonClue;
	[SerializeField] Item item;
	[SerializeField] Inventory playerInventory;
	[SerializeField] GameObject key;
	[SerializeField] GameObject pickSoundEffect;
	private bool isTaken;
	private bool isOnArea;
	private bool isCanTake;

	private void Start()
	{
		buttonClue.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnArea && buttonClue.activeInHierarchy && !isTaken)
		{
			Instantiate(pickSoundEffect);
			isTaken = true;
			playerInventory.AddItem(item);
			buttonClue.SetActive(false);
			gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
			key.SetActive(false);
			signalToRaise.Raise();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && isCanTake)
		{
			isOnArea = true;
			buttonClue.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		isOnArea = false;
		buttonClue.SetActive(false);
	}

	private IEnumerator InactiveObject()
	{
		yield return new WaitForSeconds(3.1f);
		gameObject.SetActive(false);
		if (signalToRaise != null)
		{
			signalToRaise.Raise();
		}
	}

	public void TakeKey()
	{
		if (isOnArea && buttonClue.activeInHierarchy && !isTaken)
		{
			Instantiate(pickSoundEffect);
			isTaken = true;
			playerInventory.AddItem(item);
			buttonClue.SetActive(false);
			gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
			key.SetActive(false);
			signalToRaise.Raise();
		}
	}

	public void KeyIsCanTake()
	{
		isCanTake = true;
	}
}
