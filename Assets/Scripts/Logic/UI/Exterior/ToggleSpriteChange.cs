using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 实现Toggle的sprite改变
/// </summary>
public class ToggleSpriteChange : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    private Image _targetImage = null;
    public Sprite SelectSprite = null;
    public Sprite HighLightSprite = null;
    public Sprite PressSprite = null;
    private Sprite _originSprite = null;
    private Toggle _toggle;
    private bool _isHighlight = false;
    private bool _isPress = false;
    private bool _isSelect = false;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _targetImage = _toggle.targetGraphic.GetComponent<Image>();
        _originSprite = _targetImage.sprite;
    }

    private void Update()
    {
        if (_toggle.isOn)
        {
            _isSelect = true;
        }
        else
        {
            _isSelect = false;
        }
        ChangeState();
    }

    private void ChangeState()
    {
        if (_isSelect && SelectSprite != null)
        {
            _targetImage.sprite = SelectSprite;
        }
        else if (_isPress && PressSprite != null)
        {
            _targetImage.sprite = PressSprite;
        }
        else if (_isHighlight && HighLightSprite != null)
        {
            _targetImage.sprite = HighLightSprite;
        }
        else
        {
            _targetImage.sprite = _originSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHighlight = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHighlight = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPress = false;
    }
}
