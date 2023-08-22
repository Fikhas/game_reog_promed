using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int numberOfShields;

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else if (itemToAdd.isShield)
        {
            numberOfShields++;
            ShieldScript.sharedInstance.AddAmount();
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        numberOfKeys = 0;
        numberOfShields = 0;
    }

    public void OnBeforeSerialize() { }
}
