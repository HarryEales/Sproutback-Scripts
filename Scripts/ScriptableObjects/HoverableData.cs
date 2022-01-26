using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableData : MonoBehaviour
{
    //0 = door
    //1 = bed
    //2 = item
    public int types;

    //leave as 0 if not needed (only used by doors)
    public int SceneLoaded;

    //position to teleport player to when opened
    public Vector2 posToTP;

    //audio clip to play when used
    public AudioClip[] soundMade;
}
