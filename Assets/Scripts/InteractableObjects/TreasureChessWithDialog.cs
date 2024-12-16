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
    private bool isOpened;
    [SerializeField] bool isCanOpen;
    private bool isOnArea;

    void Start()
    {
        placeItem.SetActive(false);
        buttonClue.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnArea && isCanOpen && PlayerMovement.sharedInstance.playerCurrentState == PlayerState.interact)
        {
            PlayerMovement.sharedInstance.animator.SetBool("isHoldItem", false);
            placeItem.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isOnArea && buttonClue.activeInHierarchy)
        {
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
        if (openChessCo != null)
        {
            StopCoroutine(openChessCo);
            openChessCo = null;
        }

        yield return new WaitForSeconds(0f);
        anim.SetBool("isOpen", true);
        Instantiate(openSoundEffect);
        Instantiate(getItemSoundEffect);
        playerInventory.AddItem(item);
        buttonClue.SetActive(false);
        PlayerMovement.sharedInstance.animator.SetBool("isHoldItem", true);
        placeItem.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        placeItem.SetActive(true);
        isOpened = true;
        gameObject.GetComponentInChildren<DialogInteractObject>().PopUpActive();
        signalToRaise.Raise();
    }

    public void ChangeOpenState()
    {
        isCanOpen = true;
    }
}
