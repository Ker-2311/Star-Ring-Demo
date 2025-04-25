using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 用于可缩放的ScrollRect
/// </summary>
public class MyScrollRect : ScrollRect
{
    private float _size = 1f;
    private float Minsize = 0.5f;
    private float Maxsize = 1.5f;
    public Camera UICamera;
    public override void OnScroll(PointerEventData data)
    {
        var contentRect = content.GetComponent<RectTransform>();
        UICamera = UIManager.Instance.GetCamera();
        Vector2 prePosition;
        Vector2 curPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(contentRect,data.position, UICamera, out prePosition);
        var size = gameObject.GetComponent<ScrollSize>();
        if (size != null)
        {
            Minsize = size.Minsize;
            Maxsize = size.Maxsize;
        }
        _size += data.scrollDelta.y * 0.1f;
        //判断地图尺寸是否在范围内
        _size = Mathf.Clamp(_size, Minsize, Maxsize);
        //缩放地图
        content.localScale = new Vector3(_size, _size, 1);
        //变换坐标，以鼠标点缩放
        RectTransformUtility.ScreenPointToLocalPointInRectangle(contentRect, data.position, UICamera, out curPosition);
        var deltaPosition = curPosition - prePosition;
        contentRect.anchoredPosition += deltaPosition;
    }
}

