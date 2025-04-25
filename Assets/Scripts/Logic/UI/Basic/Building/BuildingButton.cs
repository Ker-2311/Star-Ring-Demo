using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    private Button _button;
    private Building _building;
    private GameObject _icon;
    private GameObject _slider;
    //格子正在被建造
    private bool _isBuilding = false;
    //格子已经被建造了
    private bool _isBuilded = false;

    private void Awake()
    {
        _icon = transform.Find("Icon").gameObject;
        _slider = transform.Find("Slider").gameObject;
        _button = gameObject.GetComponent<Button>();

        _button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        if (_isBuilding)
        {
            _slider.SetActive(true);
            _slider.GetComponent<Slider>().maxValue = _building.buildingInfo.BuildingTime;
            _slider.GetComponent<Slider>().value = _building.buildingInfo.BuildingTime - _building.buildingRemainTime;
        }
        else
        {
            _slider.SetActive(false);
        }
    }

    public void Initialize(Building building)
    {
        _building = building;
    }

    /// <summary>
    /// 当六边形按钮被点击时
    /// </summary>
    private void OnButtonClick()
    {
        if (!_isBuilded && !_isBuilding)
        {
            var buildingListPanel = PanelMgr.Instance.Push("Prefabs/UI/Basic/Influence/Building/BuildingListPanel");
            buildingListPanel.GetComponent<BuildingListPanel>().Init(gameObject);
        }
    }

    public void StartBuild(Building building)
    {
        _building = building;
        OnBuilding();
    }

    /// <summary>
    /// 正在建造中状态
    /// </summary>
    public void OnBuilding()
    {
        _isBuilding = true;
        _icon.gameObject.SetActive(true);
        var image = _icon.GetComponent<Image>();
        image.sprite = BuildingMgr.Instance.GetBuildingIcon();
    }

    /// <summary>
    /// 建造完成状态
    /// </summary>
    public void Builded()
    {
        _isBuilding = false;
        _isBuilded = true;
        _building.builded = true;
        _icon.gameObject.SetActive(true);
        var image = _icon.GetComponent<Image>();
        image.sprite = _building.icon;
    }

    /// <summary>
    /// 查询该按钮是否有建筑
    /// </summary>
    /// <returns></returns>
    public bool IsBuilded()
    {
        if (_isBuilded || _isBuilding)
        {
            return true;
        }
        return false;
    }
}
