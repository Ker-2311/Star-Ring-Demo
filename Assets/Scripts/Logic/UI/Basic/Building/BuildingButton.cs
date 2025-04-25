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
    //�������ڱ�����
    private bool _isBuilding = false;
    //�����Ѿ���������
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
    /// �������ΰ�ť�����ʱ
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
    /// ���ڽ�����״̬
    /// </summary>
    public void OnBuilding()
    {
        _isBuilding = true;
        _icon.gameObject.SetActive(true);
        var image = _icon.GetComponent<Image>();
        image.sprite = BuildingMgr.Instance.GetBuildingIcon();
    }

    /// <summary>
    /// �������״̬
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
    /// ��ѯ�ð�ť�Ƿ��н���
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
