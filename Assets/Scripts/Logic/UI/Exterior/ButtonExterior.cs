using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonExterior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action PointerEnterCallback;
    public Action PointerExitCallback;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter(PointerEnterCallback);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit(PointerExitCallback);
    }

    public void PointerExit(Action callback)
    {
        if (callback != null)
        {
            callback();
        }
        
    }

    public void PointerEnter(Action callback)
    {
        if (callback != null)
        {
            callback();
        }
    }
}
