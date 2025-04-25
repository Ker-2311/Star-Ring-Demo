using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������湦�ܰ�ť����
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
    /// �������ܰ�ť
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
