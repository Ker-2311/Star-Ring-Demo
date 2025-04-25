using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePositionControll : MonoBehaviour
{
    public GameObject _curTech;
    public GameObject _frontTech;

    private void Update()
    {
        if (_frontTech != null)
        {
            AdjustLine(_curTech.GetComponent<RectTransform>().anchoredPosition,
                _frontTech.GetComponent<RectTransform>().anchoredPosition, gameObject);
        }
    }

    public void Init(GameObject curTech,GameObject frontTech)
    {
        _curTech = curTech;
        _frontTech = frontTech;
    }

    /// <summary>
    /// ����λ����������֮��ֱ��
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    private void AdjustLine(Vector2 start, Vector2 end, GameObject line)
    {
        RectTransform rect = line.GetComponent<RectTransform>();
        //����λ�úͽǶ�
        rect.anchoredPosition = (start + end) / 2;
        rect.rotation = Quaternion.AngleAxis(GetAngle(start, end), Vector3.forward);
        //�����߶�ͼƬ��С
        var distance = Vector2.Distance(end, start);
        var size = new Vector2(Mathf.Max(1, distance), rect.sizeDelta.y);
        rect.sizeDelta = size;
    }

    /// <summary>
    /// ��ȡ���������֮��ļн�
    /// </summary>
    /// <param name="start">��ʼ��</param>
    /// <param name="end">������</param>
    /// <returns></returns>
    private float GetAngle(Vector2 start, Vector2 end)
    {
        var dir = end - start;
        var angle = Vector2.SignedAngle(Vector2.right, dir);
        return angle;
    }
}
