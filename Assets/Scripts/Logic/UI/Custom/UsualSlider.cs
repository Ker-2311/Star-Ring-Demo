using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsualSlider : MonoBehaviour
{
    private Slider _slider;
    private Sprite _originSprite;
    public Sprite _finishSprite;
    private GameObject _fill;
    private void Awake()
    {
        _fill = transform.Find("Fill Area/Fill").gameObject;
        _originSprite = _fill.GetComponent<Image>().sprite;
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (_slider!=null && _finishSprite!=null)
        {
            if (_slider.value / _slider.maxValue >= 1)
            {
                _fill.GetComponent<Image>().sprite = _finishSprite;
            }
            else
            {
                _fill.GetComponent<Image>().sprite = _originSprite;
            }
        }
    }
}
