using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaycastEvents : MonoBehaviour
{
    public UnityEvent MouseEnter = default;
    public UnityEvent MouseExit = default;
    public UnityEvent MouseClick = default;

    private bool _IsMouseOver = false;

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;


    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        bool wasMouseOver = _IsMouseOver;
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        _IsMouseOver = false;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == this.gameObject)
            {
                _IsMouseOver = true;

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    MouseClick?.Invoke();
                }
            }
        }

        if (_IsMouseOver && !wasMouseOver) MouseEnter?.Invoke();
        if (!_IsMouseOver && wasMouseOver) MouseExit?.Invoke();
    }
}