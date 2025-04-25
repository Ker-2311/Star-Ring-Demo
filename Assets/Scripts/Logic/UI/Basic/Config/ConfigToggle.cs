using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfigToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected GameObject _infoPanel;
    //如果装备已经安装,这是安装的格子
    protected GameObject _installGrid;

    /// <summary>
    /// 安装装备
    /// </summary>
    /// <param name="gird"></param>
    public void InstallEquipment(GameObject gird)
    {
        _installGrid = gird;
    }

    /// <summary>
    /// 卸下装备
    /// </summary>
    public void RemoveEquipment()
    {
        _installGrid = null;
    }

    /// <summary>
    /// 获得安装的格子
    /// </summary>
    /// <returns></returns>
    public GameObject GetGrid()
    {
        return _installGrid;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_infoPanel != null)
        {
            _infoPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_infoPanel != null)
        {
            _infoPanel.SetActive(false);
        }
    }
}
