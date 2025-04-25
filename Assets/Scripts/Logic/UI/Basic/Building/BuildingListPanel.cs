using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;

public class BuildingListPanel : BasePanel
{
    private GameObject _buttonGroupContent;
    private GameObject _buildingListToggle;
    private GameObject _infoPanel;
    private GameObject _buildableContent;
    private GameObject _info;
    private GameObject _gridObject;
    private Sprite[] _icons;

    public void Init(GameObject grid)
    {
        base.Init();
        _gridObject = grid;
        UpdateContent(BuildingType.Sources);
    }

    public override void Awake()
    {
        base.OnEnter();
        _infoPanel = transform.Find("InfoPanel").gameObject;
        _info = _infoPanel.transform.Find("Info").gameObject;
        _buttonGroupContent = transform.Find("ScrollRect/Viewport/Content").gameObject;
        _buildingListToggle = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/Building/BuildingListToggle");
        _buildableContent = transform.Find("BuildableList/Grid/Content").gameObject;
        _icons = Resources.LoadAll<Sprite>("Image/Icon/Building");

        var sourcesButton = _buttonGroupContent.transform.Find("Sources").GetComponent<Toggle>();
        var functionButton = _buttonGroupContent.transform.Find("Function").GetComponent<Toggle>();

        sourcesButton.onValueChanged.AddListener((bool Ison) => OnToggleValueChange(sourcesButton.gameObject,
            BuildingType.Sources, Ison));
        functionButton.onValueChanged.AddListener((bool Ison) => OnToggleValueChange(functionButton.gameObject,
            BuildingType.Function, Ison));

        //OnToggleValueChange(functionButton.gameObject, BuildingType.Function, true);
    }

    /// <summary>
    /// 当按下按钮时改变内容界面
    /// </summary>
    /// <param name="content"></param>
    /// <param name="Ison"></param>
    private void UpdateContent(BuildingType buildingType)
    {
        _buildableContent.DestroyChilds();
        var buildableList = BuildingMgr.Instance.GetBuildableList();
        foreach (var buildingID in buildableList)
        {
            var info = BuildingTable.Instance[buildingID];
            if (info.BuildingType == buildingType)
            {
                var toggle = ResMgr.Instance.GetInstance(_buildingListToggle, _buildableContent.transform);
                var toggleComponent = toggle.GetComponent<Toggle>();
                var ListToggle = toggle.GetComponent<BuildingListToggle>();

                ListToggle.Initialize(info, _infoPanel, _gridObject, _icons);
                toggleComponent.group = toggle.transform.parent.GetComponent<ToggleGroup>();
            }
        }
    }

    private void OnToggleValueChange(GameObject contentButton, BuildingType buildingType, bool IsOn)
    {
        if (IsOn)
        {
            contentButton.transform.Find("Check").gameObject.SetActive(true);
            UpdateContent(buildingType);
        }
        else
        {
            contentButton.transform.Find("Check").gameObject.SetActive(false);
        }
    }

}
