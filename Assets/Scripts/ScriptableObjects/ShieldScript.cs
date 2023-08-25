using System.Collections;
using TMPro;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public static ShieldScript sharedInstance;
    public TMP_Text shieldAmount;
    public Inventory inventory;
    private bool isCanUse;

    private void Start()
    {
        sharedInstance = this;
        shieldAmount.text = inventory.numberOfShields.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseShield"))
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
            StartCoroutine(ImunCo());
        }
        else
        {
            return;
        }
    }

    private IEnumerator ImunCo()
    {
        isCanUse = true;
        yield return new WaitForSeconds(10f);
        PlayerMovement.sharedInstance.playerCurrentState = PlayerState.idle;
        isCanUse = false;
    }
}
