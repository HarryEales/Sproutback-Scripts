using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private GameObject _invUI;
    private List<Transform> children;
    private Transform selTile;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in _invUI.transform)
        {
            children.Add(child);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory(InputAction.CallbackContext context) 
    {
        Debug.Log("inv working");
        //testing if inventory opened
        if (context.performed && _invUI == false)
        {
            Debug.Log("Inv opening");
            _invUI.SetActive(true);
        }
        if (context.performed && _invUI == true)
        {
            _invUI.SetActive(false);
        }
    }

    public void InvNav(InputAction.CallbackContext context)
    {
        Debug.Log("bruh");

        float scroll = context.ReadValue<float>();

        if (scroll > 0 || scroll < 0) 
        {
            MoveSelInv(scroll / 120);
        }
    }

    public void MoveSelInv(float x)
    {
        float i = 0;
        foreach (Transform child in _invUI.transform)
        {
            foreach (Transform c in child.transform)
            {
                if (c.tag == "Selected Slot")
                {
                    selTile = c;
                    break;
                }
            }

            selTile.SetParent(children[(int)i + (int)x]);

            i++;
        }
    }
}
