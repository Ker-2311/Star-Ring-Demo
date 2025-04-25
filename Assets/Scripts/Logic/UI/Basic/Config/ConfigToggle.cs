using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfigToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected GameObject _infoPanel;
    //���װ���Ѿ���װ,���ǰ�װ�ĸ���
    protected GameObject _installGrid;

    /// <summary>
    /// ��װװ��
    /// </summary>
    /// <param name="gird"></param>
    public void InstallEquipment(GameObject gird)
    {
        _installGrid = gird;
    }

    /// <summary>
    /// ж��װ��
    /// </summary>
    public void RemoveEquipment()
    {
        _installGrid = null;
    }

    /// <summary>
    /// ��ð�װ�ĸ���
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
