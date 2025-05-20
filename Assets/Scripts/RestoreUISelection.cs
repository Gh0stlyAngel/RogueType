using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestoreUISelection : MonoBehaviour
{
    private GameObject lastSelected;
    private void Start()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    private void Update()
    {
        var current = EventSystem.current.currentSelectedGameObject;

        if (current == null && lastSelected != null)
        {
            if (MouseClickedOutsideUI())
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
        else if(current != null)
        {
            lastSelected = current;
        }
    }

    private bool MouseClickedOutsideUI()
    {
        return (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame ||
               UnityEngine.InputSystem.Mouse.current.rightButton.wasPressedThisFrame ||
               UnityEngine.InputSystem.Mouse.current.middleButton.wasPressedThisFrame) &&

               !EventSystem.current.IsPointerOverGameObject();
    }
}
