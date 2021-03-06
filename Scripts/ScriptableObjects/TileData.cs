using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public bool canBeBroken;
    public bool dropItemWhenBroken;
    public Dictionary<InventoryItem, int> drops;
}
