using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] Signal signalToRaise;
    [SerializeField] GameObject buttonClue;
    [SerializeField] Item item;
    [SerializeField] Inventory playerInventory;
    private bool isCanTake;
    private bool isOnArea;

    private void Start()
    {
        buttonClue.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnArea)
        {
            playerInventory.AddItem(item);
            buttonClue.SetActive(false);
            gameObject.GetComponentInChildren<DialogPopUp>().PopUpActiveWithTime(item.itemDescription, 5f);
            StartCoroutine(InactiveObject());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
        yield return new WaitForSeconds(5.1f);
        gameObject.SetActive(false);
        if (signalToRaise != null)
        {
            signalToRaise.Raise();
        }
    }
}