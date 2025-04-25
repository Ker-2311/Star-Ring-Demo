using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Option : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{
    private Toggle toggle;
    public Action optionOnClick;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        optionOnClick();
    }
}
