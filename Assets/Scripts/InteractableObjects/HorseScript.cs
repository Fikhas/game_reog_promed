using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class HorseScript : MonoBehaviour
{
	[SerializeField] Signal signalToRaise;
	[SerializeField] GameObject buttonClue;
	[SerializeField] Item item;
	[SerializeField] Inventory playerInventory;
	[SerializeField] GameObject horse;
	[SerializeField] GameObject pickSoundEffect;
	private bool isCanTake;
	private bool isTaken;
	private bool isOnArea;

	private void Start()
	{
		buttonClue.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnArea && buttonClue.activeInHierarchy && !isTaken && isCanTake)
		{
			Instantiate(pickSoundEffect);
			isTaken = true;
			playerInventory.AddItem(item);
			buttonClue.SetActive(false);
			gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
			horse.SetActive(false);
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

	public void TakeHorse()
	{
		if (isOnArea && buttonClue.activeInHierarchy && !isTaken && isCanTake)
		{
			Instantiate(pickSoundEffect);
			isTaken = true;
			playerInventory.AddItem(item);
			buttonClue.SetActive(false);
			gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
			horse.SetActive(false);
			signalToRaise.Raise();
		}
	}

	public void HorseIsCanTake()
	{
		isCanTake = true;
	}
}
