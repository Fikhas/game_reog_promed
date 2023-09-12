using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] TMP_Text shieldAmount;
    [SerializeField] Inventory inventory;
    [SerializeField] Signal imunActiveSignal;
    public static ShieldScript sharedInstance;
    private bool isCanUse;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sharedInstance = this;
        shieldAmount.text = inventory.numberOfShields.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseShield") && !isCanUse && player.GetComponent<PlayerMovement>().playerCurrentState != PlayerState.interact)
        {
            UseShield();
        }
    }

    public void AddAmount()
    {
        shieldAmount.text = inventory.numberOfShields.ToString();
    }

    public void SubAmount(int amount)
    {
        inventory.numberOfShields -= amount;
        shieldAmount.text = inventory.numberOfShields.ToString();
    }

    private void UseShield()
    {
        if (inventory.numberOfShields != 0)
        {
            PlayerMovement.sharedInstance.playerCurrentState = PlayerState.imun;
            SubAmount(1);
            imunActiveSignal.Raise();
            StartCoroutine(ImunCo());
        }
        else
        {
            return;
        }
    }

    private IEnumerator ImunCo()
    {
        // Instantiate(player.GetComponent<PlayerMovement>().imunAnim, player.transform, worldPositionStays: false);
        isCanUse = true;
        yield return new WaitForSeconds(3f);
        PlayerMovement.sharedInstance.playerCurrentState = PlayerState.idle;
        isCanUse = false;
    }
}
