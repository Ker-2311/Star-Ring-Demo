using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;

public class OrdnancePanel : BasePanel
{
    private GameObject _equipment;
    private GameObject _produce;
    private GameObject _viewportContent;
    private GameObject _equipmentButtonContent;
    private GameObject _produceButtonContent;
    private GameObject _equipmentGridContent;
    private GameObject _produceGridContent;
    private GameObject _grid;
    private GameObject _infoPanel;
    private Animation _animation;


    public override void OnEnter()
    {
        base.OnEnter();

        _infoPanel = transform.Find("InfoPanel").gameObject;
        _equipment = transform.Find("Equipment").gameObject;
        _produce = transform.Find("Produce").gameObject;
        _viewportContent = transform.Find("ScrollRect/Viewport/Content").gameObject;
        _equipmentButtonContent = _equipment.transform.Find("ButtonGroup/Content").gameObject;
        _produceButtonContent = _produce.transform.Find("ButtonGroup/Content").gameObject;
        _equipmentGridContent = _equipment.transform.Find("Grid/Content").gameObject;
        _produceGridContent = _produce.transform.Find("Grid/Content").gameObject;
        _grid = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Building/EquipmentGrid");
        _animation = GetComponent<Animation>();

        var equipmentButton = _viewportContent.transform.Find("Equipment").GetComponent<Toggle>();
        var produceButton = _viewportContent.transform.Find("Produce").GetComponent<Toggle>();

        equipmentButton.onValueChanged.AddListener(EquipmentButtonOnClick);
        produceButton.onValueChanged.AddListener(ProduceButtonOnClick);

        //����Ϣ�����˳���ť��Ӻ���
        _infoPanel.transform.Find("CloseButton").GetComponent<Button>().
            onClick.AddListener(() => _animation.Play("OrdanceInfoPanelClose"));

        //��ʼ��������
        AddContent(_equipmentGridContent, EquipmentType.����);
        //װ�������������ť
        var equipment_weaponToggle = _equipmentButtonContent.transform.Find("Weapon").GetComponent<Toggle>();
        var equipment_armourToggle = _equipmentButtonContent.transform.Find("Armour").GetComponent<Toggle>();
        var equipment_shieldToggle = _equipmentButtonContent.transform.Find("Shield").GetComponent<Toggle>();
        var equipment_componentToggle = _equipmentButtonContent.transform.Find("Component").GetComponent<Toggle>();

        equipment_weaponToggle.onValueChanged.AddListener((isOn) => ContentToggleOnClick(isOn,
            _equipmentGridContent, EquipmentType.����));
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="content"></param>
    /// <param name="type"></param>
    private void AddContent(GameObject content, EquipmentType type)
    {
        var eqData = EquipmentMgr.Instance.GetEquipmentData();
        //װ������������
        if (content == _equipmentGridContent)
        {
            foreach (var valuePair in eqData)
            {
                if(valuePair.Value.EquipmentType == type && !valuePair.Value.IsLock)
                {
                    GenerateGrid(content, valuePair.Key);
                }
            }
        }

        else if(content == _produceGridContent)
        {
            foreach (var valuePair in eqData)
            {
                if (valuePair.Value.EquipmentType == type && !valuePair.Value.IsLock && !valuePair.Value.IsProduce)
                {
                    GenerateGrid(content, valuePair.Key);
                }
            }
        }
    }

    /// <summary>
    /// ����һ��װ������
    /// </summary>
    /// <param name="content"></param>
    /// <param name="id"></param>
    private void GenerateGrid(GameObject content, string id)
    {
        var grid = ResMgr.Instance.GetInstance(_grid, content.transform);
        grid.GetComponent<Button>().onClick.AddListener(ShowInfoPanel);
        grid.name = id;

        //var info = (WeaponInfo)Data.instance.EquipmentData[id].EquipmentInfo;
        //grid.transform.Find("Name").GetComponent<Text>().text = info.Name;
    }

    private void ClearContent(GameObject content)
    {
        content.DestroyChilds();
    }

    /// <summary>
    /// ����ť�����������ݵ�Content
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="content"></param>
    /// <param name="type"></param>
    private void ContentToggleOnClick(bool isOn,GameObject content,EquipmentType type)
    {
        if (isOn)
        {
            AddContent(content,type);
        }
        else
        {
            ClearContent(content);
        }
    }

    /// <summary>
    /// �л�����������
    /// </summary>
    /// <param name="isOn"></param>
    private void ProduceButtonOnClick(bool isOn)
    {
        if (isOn)
        {
            _produce.SetActive(true);
        }
        else
        {
            _produce.SetActive(false);
        }
    }

    /// <summary>
    /// �л���װ������
    /// </summary>
    /// <param name="isOn"></param>
    private void EquipmentButtonOnClick(bool isOn)
    {
        if (isOn)
        {
            _equipment.SetActive(true);
        }
        else
        {
            _equipment.SetActive(false);
        }
    }

    private void ShowInfoPanel()
    {
        //�����Ϣ����û�м����򲥷���ʾ����
        if (!_infoPanel.activeInHierarchy)
        {
            _animation.Play("OrdanceInfoPanelShow");
        }
    }
}
