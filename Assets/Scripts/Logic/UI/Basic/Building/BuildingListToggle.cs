using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

class BuildingListToggle:MonoBehaviour
{
    private BuildingInfo _buildingInfo;
    private GameObject _infoPanel;
    private GameObject _grid;
    private List<Sprite> _icons;
    public void Initialize(BuildingInfo info,GameObject infoPanel,GameObject grid,Sprite[] icons)
    {
        _buildingInfo = info;
        _infoPanel = infoPanel;
        _grid = grid;
        _icons = icons.ToList();

        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnGridToggleValueChange);
        toggle.group = transform.parent.GetComponent<ToggleGroup>();

        transform.Find("BuildingButton").GetComponent<Button>().onClick.AddListener(OnBuildingButtonClick);

        var name = transform.Find("Name").GetComponent<Text>();
        var icon = transform.Find("Icon").GetComponent<Image>();
        var time = transform.Find("Time").GetComponent<Text>();
        var progressBar = transform.Find("ProgressBar").GetComponent<Slider>();

        name.text = _buildingInfo.Name;
        icon.sprite = _icons.Find(t => t.name == (_buildingInfo.Name + "1级"));
        time.text = "完成所需星历时间:" + "<color=#3CB9FF>" + _buildingInfo.BuildingTime + "天</color>";
    }
    private void OnGridToggleValueChange(bool IsOn)
    {
        var info = _infoPanel.transform.Find("Info");
        var name = info.Find("Name").GetComponent<Text>();
        var icon = info.Find("Icon").GetComponent<Image>();
        var Description = info.Find("Description").GetComponent<Text>();
        var MaxRank = info.Find("MaxRank").GetComponent<Text>();
        var StationMaxBuilded = info.Find("StationMaxBuilded").GetComponent<Text>();
        var SpaceMaxBuilded = info.Find("SpaceMaxBuilded").GetComponent<Text>();
        var BuildingCost = info.Find("BuildingCost").GetComponent<Text>();
        var BuildingSourcesCost = info.Find("BuildingSourcesCost").GetComponent<Text>();
        if (IsOn)
        {
            info.gameObject.SetActive(true);
            name.text = _buildingInfo.Name;

            Description.text = _buildingInfo.Descript;
            MaxRank.text = _buildingInfo.MaxRank.ToString();
            StationMaxBuilded.text = _buildingInfo.StationMaxBuilded.ToString();
            SpaceMaxBuilded.text = _buildingInfo.SpaceMaxBuilded.ToString();
            BuildingCost.text = _buildingInfo.Cost.ToString();
            icon.sprite = _icons.Find(t => t.name == (_buildingInfo.Name + "1级"));
            //BuildingSourcesCost.text = _buildingInfo.BuildingSourcesCost.ToString();
        }
        else
        {
            info.gameObject.SetActive(false);
        }
    }

    private void OnBuildingButtonClick()
    {

        var gridButton = _grid.GetComponent<BuildingButton>();
        var station = StationMgr.Instance.GetCurStation();
        var building = BuildingMgr.Instance.StartBuildConstruct(station, _buildingInfo.ID, _grid.name);
        gridButton.StartBuild(building);
        //退出BuildingListPanel
        PanelMgr.Instance.Pop();
    }
}

