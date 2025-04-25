using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,
    IPointerDownHandler,IPointerUpHandler
{
    public Sprite HighLightSpirite = null;
    public Sprite SelectSprite = null;
    public Sprite OriginSprite = null;
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HighLightSpirite != null)
        {
            _image.sprite = HighLightSpirite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HighLightSpirite != null)
        {
            _image.sprite = OriginSprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SelectSprite != null)
        {
            _image.sprite = SelectSprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SelectSprite != null)
        {
            _image.sprite = OriginSprite;
        }
    }
}
