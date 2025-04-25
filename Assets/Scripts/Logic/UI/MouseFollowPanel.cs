using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڸ�������������
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
        //�Ը�����Ϊê�����ĵ��������
        Vector2 mousePosInParent;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent.GetComponent<RectTransform>(),
            Input.mousePosition, UIManager.Instance.GetCamera(), out mousePosInParent);
        float width = rect.sizeDelta.x;
        float height = rect.sizeDelta.y;
        //���ó�����Ļ��Ե�Զ�����
        Vector2 pivot = new Vector2();

        if (Input.mousePosition.x + width <= Screen.width) // ���ȿ���
        {
            pivot.x = 0;
        }
        else // ��
        {
            pivot.x = 1;
        }

        if (Input.mousePosition.y - height >= 0) // ���ȿ���
        {
            pivot.y = 1;
        }
        else // ��
        {
            pivot.y = 0;
        }
        rect.pivot = pivot;
        rect.anchoredPosition = mousePosInParent;
    }
}
