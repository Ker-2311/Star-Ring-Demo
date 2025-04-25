using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造界面功能按钮控制
/// </summary>
public class BuildingFunctionButton : MonoBehaviour
{
    private GameObject _icon;
    public string functionBuildingID;
    public Sprite unlockSprite;
    private void Awake()
    {
        _icon = transform.Find("Icon").gameObject;
    }
    private void Update()
    {
        if (StationMgr.Instance.GetCurStation() != null)
        {
            if (BuildingMgr.Instance.IsBuildingExist(StationMgr.Instance.GetCurStation().StationID, functionBuildingID))
            {
                ActivateButton();
            }
        }
    }

    /// <summary>
    /// 解锁功能按钮
    /// </summary>
    private void ActivateButton()
    {
        _icon.SetActive(true);
        GetComponent<Image>().sprite = unlockSprite;
        OpenFunctionPanel();
    }

    private void OpenFunctionPanel()
    {

    }
}
