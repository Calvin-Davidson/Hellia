using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent MouseEnter;
    public UnityEvent MouseExit;
   
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter?.Invoke();
    }
}
