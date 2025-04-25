using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBSlider : MonoBehaviour
{
    private Slider _slider;
    private Text _valueText;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _valueText = transform.Find("Value/Text").GetComponent<Text>();
    }
    private void Update()
    {
        
    }
}
