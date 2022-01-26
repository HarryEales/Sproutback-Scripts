using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public int amount;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = referenceItem.icon;
    }

    public void OnHandlePickupItem()
    {
        inventorySystem.current.Add(referenceItem, amount);

        Destroy(gameObject);
    }
}
