using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public float numberID;
    public string nameID;
    public string displayName;
    public Sprite icon;

    public bool destoryOnUse;

    public GameObject _gameObjectWithScript;
}
