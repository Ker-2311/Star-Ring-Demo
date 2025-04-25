using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : BasePanel
{
    private GameObject _equipmentContent;
    private GameObject _equipmentPanel;
    private Animator _equipmentPanelAnimator;
    private GameObject _configTogglePrefab;
    private GameObject _chanceType;
    private GameObject _equipmentGrid;
    public override void Awake()
    {
        _equipmentContent = transform.Find("EquipmentChancePanel/ScrollRect/Viewport/Content").gameObject;
        _equipmentPanel = transform.Find("EquipmentPanel").gameObject;
        _equipmentPanelAnimator = _equipmentPanel.GetComponent<Animator>();
        _configTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Config/ConfigToggle");
        _equipmentGrid = _equipmentPanel.transform.Find("EquipmentGrid").gameObject;
        _chanceType = transform.Find("EquipmentChancePanel/ChanceEquipType").gameObject;

        _chanceType.transform.Find("WeaponToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => 
            EquipmentChanceTypeToggleOnValueChange(isOn, EquipmentMgr.Instance.GetEquipmentsList(EquipmentType.����)));
        //��IconButton��Ӱ�ť�¼�
        foreach (var eqBtn in _equipmentPanel.transform.Find("EquipmentGrid/IconButton").gameObject.GetAllChilds())
        {
            eqBtn.GetComponent<Button>().onClick.AddListener(EquipmentPanelButtonOnClick);
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EquipmentChanceTypeToggleOnValueChange(true, EquipmentMgr.Instance.GetEquipmentsList(EquipmentType.����));
    }
    /// <summary>
    /// EquimentPanel��IconButton�����
    /// </summary>
    private void EquipmentPanelButtonOnClick()
    {
        if (_equipmentPanelAnimator.GetInteger("Action") == 2)
        {
            _equipmentPanelAnimator.SetInteger("Action", 1);
        }
        else if (_equipmentPanelAnimator.GetInteger("Action") == 1)
        {
            _equipmentPanelAnimator.SetInteger("Action", 2);
        }
    }

    /// <summary>
    /// װ�����ఴťֵ�ı�
    /// </summary>
    /// <param name="isOn"></param>
    private void EquipmentChanceTypeToggleOnValueChange(bool isOn, List<IEquipment> equipments)
    {
        if (isOn)
        {
            ContentChange(equipments);
        }
    }

    /// <summary>
    /// �ı�װ������
    /// </summary>
    /// <param name="equipments"></param>
    private void ContentChange(List<IEquipment> equipments)
    {
        _equipmentContent.DestroyChilds();
        foreach (var eq in equipments)
        {
            if (eq.IsLock)
            {
                var configToggle = ResMgr.Instance.GetInstance(_configTogglePrefab, _equipmentContent.transform);
                switch (eq.EquipmentType)
                {
                    case EquipmentType.����:
                        {
                            var installedEquipment = PlayerShipMgr.Instance.GetInstalledWeaponList();
                            var weaponToggle = configToggle.AddComponent<WeaponToggle>();
                            //����Ѱ�װװ���б��д��ڴ�װ��
                            if (installedEquipment.Contains(eq as Weapon))
                            {
                                configToggle.GetComponent<Toggle>().isOn = true;
                            }
                            weaponToggle.Init(eq as Weapon,transform.Find("EquipmentChancePanel/InfoPanel").gameObject);
                            //ConfigToggle��ӵ���¼������ʱװ��
                            configToggle.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => WeaponToggleOnValueChange(isOn
                                , weaponToggle));
                            break;
                        }
                }

            }
        }
    }

    private void WeaponToggleOnValueChange(bool isOn,WeaponToggle weaponToggle)
    {
        //ѡ�е���������
        //var chanceWeaponGrid = _equipmentGrid.GetComponent<ToggleGroup>().GetFirstActiveToggle().GetComponent<WeaponGrid>();
        //if (isOn)
        //{
        //    //��ѡ�еĸ��ӽ���װ��
        //    var weapon = weaponToggle.GetWeapon();
        //    chanceWeaponGrid.EquipWeapon(weapon);
        //    weaponToggle.InstallEquipment(chanceWeaponGrid.gameObject);
        //    PlayerShipMgr.Instance.InstallWeapon(weapon,chanceWeaponGrid.name);
        //}
        //else
        //{
        //    if (weaponToggle.GetGrid() != null)
        //    {
        //        weaponToggle.GetGrid().GetComponent<WeaponGrid>().UnEquipWeapon();
        //    }

        //}
    }
}
