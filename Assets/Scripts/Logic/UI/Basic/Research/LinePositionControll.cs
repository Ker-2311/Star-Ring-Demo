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
    /// 调整位置生成两点之间直线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    private void AdjustLine(Vector2 start, Vector2 end, GameObject line)
    {
        RectTransform rect = line.GetComponent<RectTransform>();
        //设置位置和角度
        rect.anchoredPosition = (start + end) / 2;
        rect.rotation = Quaternion.AngleAxis(GetAngle(start, end), Vector3.forward);
        //设置线段图片大小
        var distance = Vector2.Distance(end, start);
        var size = new Vector2(Mathf.Max(1, distance), rect.sizeDelta.y);
        rect.sizeDelta = size;
    }

    /// <summary>
    /// 获取两个坐标点之间的夹角
    /// </summary>
    /// <param name="start">起始点</param>
    /// <param name="end">结束点</param>
    /// <returns></returns>
    private float GetAngle(Vector2 start, Vector2 end)
    {
        var dir = end - start;
        var angle = Vector2.SignedAngle(Vector2.right, dir);
        return angle;
    }
}
