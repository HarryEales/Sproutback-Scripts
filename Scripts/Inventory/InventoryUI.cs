using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        inventorySystem.current.onInventoryChange += OnUpdateInventory;
    }

    private void OnUpdateInventory(bool no)
    {
        foreach (Transform t in transform)
        {
            foreach (Transform child in t.transform)
            {
                if (child.tag == "ItemSlot")
                {
                    Destroy(child.gameObject);
                }
            }
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in inventorySystem.current.inventory)
        {
            StartCoroutine(AddInventorySlot(item));
        }
    }

    IEnumerator AddInventorySlot(InventoryItem item)
    {
        yield return new WaitForSeconds(0.001f);

        GameObject obj = Instantiate(_slotPrefab);

        foreach (Transform child in transform)
        {
            bool isFree;
            isFree = true;
            foreach (Transform c in child.transform)
            {
                if (c.tag == "ItemSlot")
                    isFree = false;
            }

            if (isFree)
            {
                obj.transform.SetParent(child.transform, false);
                break;
            }
        }

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
