using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool hideOnEnable = false;

    private void Awake()
    {
        if (hideOnEnable) HideMouse();
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;   
    }
}
