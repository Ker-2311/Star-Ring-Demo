using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于跟随鼠标的悬浮窗
/// </summary>
public class MouseFollowPanel : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<RectTransform>().pivot = Vector2.up;
    }
    private void Update()
    {
        RectTransform rect = GetComponent<RectTransform>();
        //以父对象为锚点中心的鼠标坐标
        Vector2 mousePosInParent;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent.GetComponent<RectTransform>(),
            Input.mousePosition, UIManager.Instance.GetCamera(), out mousePosInParent);
        float width = rect.sizeDelta.x;
        float height = rect.sizeDelta.y;
        //设置超出屏幕边缘自动变向
        Vector2 pivot = new Vector2();

        if (Input.mousePosition.x + width <= Screen.width) // 优先靠右
        {
            pivot.x = 0;
        }
        else // 左
        {
            pivot.x = 1;
        }

        if (Input.mousePosition.y - height >= 0) // 优先靠下
        {
            pivot.y = 1;
        }
        else // 上
        {
            pivot.y = 0;
        }
        rect.pivot = pivot;
        rect.anchoredPosition = mousePosInParent;
    }
}
