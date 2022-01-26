using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Text _stacksize;

    [SerializeField]
    private GameObject _stackSizeObj;

    public void Set(InventoryItem item)
    {
        _icon.sprite = item.data.icon;
        if (item.stackSize <= 1)
        {
            _stackSizeObj.SetActive(false);
            return;
        }

        _stacksize.text = item.stackSize.ToString();
    }
}
