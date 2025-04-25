using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WareHouseToggle : ItemToggle
{
    public bool IsStore = true;
    private bool _isOn = false;
    public override void OnValueChanged(bool IsOn)
    {
        base.OnValueChanged(IsOn);
        _isOn = IsOn;
        if (IsOn)
        {
            if (transform.parent.parent.parent.parent.name == "Inventory")
            {
                IsStore = true;
            }
            else
            {
                IsStore = false;
            }
        }
    }

    public override void Update()
    {
        if (_isOn)
        {
            var slider = _description.transform.Find("Slider").GetComponent<Slider>();

            slider.minValue = 1;
            slider.maxValue = item.count;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {

    }

    public override void OnPointerExit(PointerEventData eventData)
    {

    }
}
