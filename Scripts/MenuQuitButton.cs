using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuQuitButton : MonoBehaviour
{
    public void OnPressed()
    {
        Debug.Log("Quiting");
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
