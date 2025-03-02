using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
	[SerializeField] private GameObject clue;
	[SerializeField] private GameObject npcDialogue;
	[SerializeField] private Signal dialogueOnSignal;
	[SerializeField] private Signal dialogueOffSignal;

	private bool isOnArea;
	private bool isRaiseOffSignal;
	private GameObject npcDialogueActive;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnArea)
		{
			if (npcDialogueActive == null)
			{
				npcDialogueActive = Instantiate(npcDialogue);
				if (!npcDialogueActive.activeInHierarchy) npcDialogue.SetActive(true);
				isRaiseOffSignal = true;
				dialogueOnSignal.Raise();
			}
		}

		if (npcDialogueActive != null)
		{
			if (!npcDialogueActive.activeInHierarchy && isRaiseOffSignal)
			{
				dialogueOffSignal.Raise();
				isRaiseOffSignal = false;
				Destroy(npcDialogueActive);
				npcDialogueActive = null;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			clue.SetActive(true);
			isOnArea = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			clue.SetActive(false);
			isOnArea = false;
		}
	}
}
