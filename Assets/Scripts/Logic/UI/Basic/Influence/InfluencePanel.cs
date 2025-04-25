using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;
using System;

public class InfluencePanel : BasePanel
{
    //ForcePanel
    private GameObject _forcePanel;
    private Toggle _forceChanceToggle;
    private GameObject _forceTogglePrefab;
    private GameObject _forceContent;
    private GameObject _forceInfo;
    private GameObject _iconPanel;
    private List<Sprite[]> _playerForceIcons = new List<Sprite[]>();
    private GameObject _chanceOption;
    private GameObject _colorSlider;
    //StationPanel
    private GameObject _stationPanel;
    private Toggle _stationChanceToggle;
    private GameObject _stationContent;
    private GameObject _stationTogglePrefab;
    private Button _enterStationButton;
    private GameObject _stationInfo;

    public override void Awake()
    {
        base.OnEnter();
        _forcePanel = transform.Find("ForcePanel").gameObject;
        _stationPanel = transform.Find("StationPanel").gameObject;
        _forceChanceToggle = transform.Find("ButtonGroup/ForceToggle").GetComponent<Toggle>();
        _stationChanceToggle = transform.Find("ButtonGroup/StationToggle").GetComponent<Toggle>();
        _forceContent = transform.Find("ForcePanel/Scroll View/Viewport/Content").gameObject;
        _stationContent = transform.Find("StationPanel/Scroll View/Viewport/Content").gameObject;
        _enterStationButton = transform.Find("StationPanel/Info/EnterBuildingButton").GetComponent<Button>();
        _forceTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/Force/ForceToggle");
        _stationTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/Station/StationToggle");
        _forceInfo = _forcePanel.transform.Find("ForceInfo").gameObject;
        _iconPanel = _forcePanel.transform.Find("IconPanel").gameObject;
        _chanceOption = _iconPanel.transform.Find("ChanceOption").gameObject;
        _colorSlider = _iconPanel.transform.Find("ColorSlider").gameObject;
        _stationInfo = _stationPanel.transform.Find("Info").gameObject;

        //读取势力图标
        for (int i = 0;i<3;i++)
        {
            _playerForceIcons.Add(ResMgr.Instance.GetAllResources<Sprite>("Image/Icon/Influence/PlayerForceIcon/" + i.ToString()));
        }
        //给ColorSlider添加值变化函数
        foreach (var slider in _colorSlider.GetAllChilds())
        {
            var sliderComponent = slider.GetComponent<Slider>();
            var inputFieldComponent = slider.transform.Find("Value/InputField").GetComponent<InputField>();

            sliderComponent.onValueChanged.AddListener((float value) => IconPanelColorSliderOnValueChange(value,inputFieldComponent));
            inputFieldComponent.onValueChanged.AddListener((string value) => IconPanelColorInputFieldOnValueChange(value, sliderComponent));
        }
        //给ChanceOptionToggle添加值变化函数
        foreach (var option in _chanceOption.GetAllChilds())
        {
            var content = option.transform.Find("Scroll View/Viewport/Content").gameObject;
            foreach (var toggle in content.GetAllChilds())
            {
                toggle.GetComponent<Toggle>().onValueChanged.AddListener(IconPanelIconToggleOnValueChange);
            }
        }

        _enterStationButton.onClick.AddListener(EnterStation);
        _forceChanceToggle.onValueChanged.AddListener(ForceChanceToggleOnValueChange);
        _stationChanceToggle.onValueChanged.AddListener(StationChanceToggleOnValueChange);
        _forceInfo.transform.Find("IconPanelButton").GetComponent<Button>().onClick.AddListener(IconPanelButtonOnClick);
        _iconPanel.transform.Find("EnsureButton").GetComponent<Button>().onClick.AddListener(IconPanelEnsureButtonOnClick);
        _iconPanel.transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(IconPanelCancelButtonOnClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UpdateStationContent();
        UpdateForceContent();
    }

    /// <summary>
    /// 进入空间站界面
    /// </summary>
    private void EnterStation()
    {
        var stationToggle = _stationContent.GetComponent<ToggleGroup>().GetFirstActiveToggle().GetComponent<StationToggle>();
        StationMgr.Instance.EnterStation(stationToggle.station.StationID);
        PanelMgr.Instance.Push("Prefabs/UI/Basic/Influence/Building/BuildingPanel");
    }

    /// <summary>
    /// 更新空间站内容
    /// </summary>
    private void UpdateStationContent()
    {
        _stationContent.DestroyChilds();
        var stationList = StationMgr.Instance.GetAllStation();
        for (int i = 0; i < stationList.Count; i++)
        {
            var toggle = ResMgr.Instance.GetInstance(_stationTogglePrefab, _stationContent.transform);
            var stationToggle = toggle.GetComponent<StationToggle>();
            var toggleComponent = toggle.GetComponent<Toggle>();
            var station = stationList[i];
            toggleComponent.group = toggle.transform.parent.GetComponent<ToggleGroup>();
            toggleComponent.onValueChanged.AddListener((bool isOn) => StationToggleOnValueChange(isOn, station));

            stationToggle.Init(station);
        }
    }
    /// <summary>
    /// 更新势力内容
    /// </summary>
    private void UpdateForceContent()
    {
        _forceContent.DestroyChilds();
        var forceList = ForceMgr.Instance.GetAllForce();
        for (int i = 0; i < forceList.Count; i++)
        {
            var toggle = ResMgr.Instance.GetInstance(_forceTogglePrefab, _forceContent.transform);
            var forceToggle = toggle.GetComponent<ForceToggle>();
            var toggleComponent = toggle.GetComponent<Toggle>();
            var force = forceList[i];
            toggleComponent.group = toggle.transform.parent.GetComponent<ToggleGroup>();
            toggleComponent.onValueChanged.AddListener((bool isOn) => ForceToggleOnValueChange(isOn, force));

            forceToggle.Init(force);
        }
    }

    /// <summary>
    /// 势力面板选择Toggle
    /// </summary>
    /// <param name="isOn"></param>
    private void ForceChanceToggleOnValueChange(bool isOn)
    {
        if (isOn)
        {
            _forcePanel.SetActive(true);
        }
        else
        {
            _forcePanel.SetActive(false);
        }
    }

    /// <summary>
    /// 空间站面板选择Toggle
    /// </summary>
    /// <param name="isOn"></param>
    private void StationChanceToggleOnValueChange(bool isOn)
    {
        if (isOn)
        {
            _stationPanel.SetActive(true);
        }
        else
        {
            _stationPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 势力Toggle切换
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="force"></param>
    private void ForceToggleOnValueChange(bool isOn,Force force)
    {
        if (isOn)
        {
            _forceInfo.SetActive(true);
            var iconButton = _forceInfo.transform.Find("IconPanelButton").gameObject;
            if (force.forceInfo.ID == "000000")
            {
                iconButton.SetActive(true);
            }
            else
            {
                iconButton.SetActive(false);
            }
        }
        else
        {
            _forceInfo.SetActive(false);
        }
    }

    /// <summary>
    /// 空间站Toggle切换
    /// </summary>
    /// <param name="isOn"></param>
    private void StationToggleOnValueChange(bool isOn,Station station)
    {
        if (isOn)
        {
            _stationInfo.SetActive(true);
        }
        else
        {
            _stationInfo.SetActive(false);
        }
    }

    private void IconPanelIconToggleOnValueChange(bool isOn)
    {
        if (isOn)
        {
            IconPanelIconUpdate();
        }
    }
    /// <summary>
    /// 玩家势力Icon管理
    /// </summary>
    private void IconPanelButtonOnClick()
    {
        var playerForce = ForceMgr.Instance.GetPlayerForce();
        var icons = _iconPanel.transform.Find("Icons").gameObject;
        
        _iconPanel.SetActive(true);
        if (playerForce.playerIcon != null)
        {
            var iconInfo = playerForce.playerIcon.Split(' ');
            //指定Playericon编码为12位格式，以空格分隔
            if (iconInfo.Length == 6)
            {
                for (int i = 0; i < 3; i++)
                {
                    var icon = icons.GetAllChilds()[i].GetComponent<Image>();
                    icon.sprite = _playerForceIcons[i][Convert.ToInt32(iconInfo[i])];
                    icon.color = Convert.ToInt32(iconInfo[3]) / 255f * new Color(1, 0, 0) +
                        Convert.ToInt32(iconInfo[4]) / 255f * new Color(0, 1, 0) +
                        Convert.ToInt32(iconInfo[5]) / 255f * new Color(0, 0, 1);

                }
            }
            else
            {
                Debug.LogWarning("输入的玩家势力Icon指数格式不正确");
            }
        }
    }

    private void IconPanelEnsureButtonOnClick()
    {

    }

    private void IconPanelCancelButtonOnClick()
    {
        _iconPanel.SetActive(false);
    }

    /// <summary>
    /// RGBSlider值变化函数
    /// </summary>
    /// <param name="value"></param>
    /// <param name="inputField"></param>
    private void IconPanelColorSliderOnValueChange(float value,InputField inputField)
    {
        inputField.text = value.ToString();
        IconPanelIconUpdate();
    }

    /// <summary>
    /// RGBSlider的InputField值变化函数
    /// </summary>
    /// <param name="value"></param>
    /// <param name="slider"></param>
    private void IconPanelColorInputFieldOnValueChange(string value,Slider slider)
    {
        var valueInt = Convert.ToInt32(value);
        if (valueInt > 255)
        {
            valueInt = 255;
        }
        else if (valueInt < 0)
        {
            valueInt = 0;
        }
        slider.value = valueInt;
        IconPanelIconUpdate();
    }

    /// <summary>
    /// 更新IconPanelIcon
    /// </summary>
    private void IconPanelIconUpdate()
    {
        Color color = new Color();
        List<Sprite> sprites = new List<Sprite>();
        var icons = _iconPanel.transform.Find("Icons").gameObject;
        //获取颜色
        color.r = _colorSlider.transform.Find("R").GetComponent<Slider>().value/255f;
        color.g = _colorSlider.transform.Find("G").GetComponent<Slider>().value/255f;
        color.b = _colorSlider.transform.Find("B").GetComponent<Slider>().value/255f;
        color.a = 1;
        //获取Sprite
        for(int i =0;i<3;i++)
        {
            var option = _chanceOption.GetAllChilds()[i];
            var activateOption = option.transform.Find("Scroll View/Viewport/Content").GetComponent<ToggleGroup>().GetFirstActiveToggle();
            if (activateOption != null)
            {
                sprites.Add(_playerForceIcons[i][Convert.ToInt32(activateOption.name)]);
            }
            else
            {
                sprites.Add(_playerForceIcons[i][0]);
            }
        }
        //更新Icon
        for (int i = 0; i < 3; i++)
        {
            var icon = icons.GetAllChilds()[i].GetComponent<Image>();
            icon.sprite = sprites[i];
            icon.color = color;
        }
    }
}
