using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WareHousePanel : BasePanel
{
    private GameObject _warehouseContent;
    private GameObject _inventoryContent;
    private GameObject _itemTogglePrefab;
    private Sprite[] _icons;
    private GameObject _description;
    private Button storeAndTakeOutButton;
    private ToggleGroup _toggleGroup;

    public override void Awake()
    {
        base.OnEnter();
        _warehouseContent = transform.Find("WareHouse/ScrollRect/Viewport/Content").gameObject;
        _inventoryContent = transform.Find("Inventory/ScrollRect/Viewport/Content").gameObject;
        _icons = Resources.LoadAll<Sprite>("Image/Icon/Item");
        _itemTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/StationFunction/WareHouse/Toggle");
        _description = transform.Find("Description").gameObject;
        _toggleGroup = transform.Find("ToggleGroup").GetComponent<ToggleGroup>();

        UpdateContent(_inventoryContent, InventoryMgr.Instance.GetInventoryList(), _description);
        UpdateContent(_warehouseContent, WareHouseMgr.Instance.GetWareHouseList(), _description);

        storeAndTakeOutButton = transform.Find("Description/StoreAndTakeOutButton").GetComponent<Button>();
        storeAndTakeOutButton.onClick.AddListener(StoreAndTakeOutButtonOnClick);

    }

    /// <summary>
    /// �����б�
    /// </summary>
    /// <param name="content"></param>
    /// <param name="list"></param>
    private void UpdateContent(GameObject content,List<Item> list,GameObject description)
    {
        var childs = content.GetAllChilds();
        //�����Ӷ�����û�е����û��������е���Ʒ
        for (int i = 0; i < list.Count; i++)
        {
            if (childs.Exists(t=>(t.name == list[i].itemInfo.Name)))
            {
                continue;
            }
            //��������Toggle
            var item = ResMgr.Instance.GetInstance(_itemTogglePrefab, content.transform);
            var toggle = item.GetComponent<Toggle>();

            item.GetComponent<WareHouseToggle>().Init(list[i], description, _icons);
            toggle.group = _toggleGroup;
        }
        //������Ʒ���ݲ�ɾ���û�������û�е���Ʒ����
        foreach (var child in childs)
        {
            if (list.Exists(t=>(t.itemInfo.Name == child.name)))
            {
                child.GetComponent<WareHouseToggle>().item = list.Find(t => (t.itemInfo.Name == child.name));
            }
            else
            {
                GameObject.Destroy(child);
            }
        }
        SortContent(content);
        var firstactivateToggle = _toggleGroup.GetFirstActiveToggle();
        if (firstactivateToggle != null)
        {
            firstactivateToggle.GetComponent<WareHouseToggle>().OnValueChanged(true);
        }
        else   //���û���Ѽ���Toggle��ȡ��Description��ʾ
        {
            _description.SetActive(false);
        }
    }
    /// <summary>
    /// �洢��ȡ����ť
    /// </summary>
    private void StoreAndTakeOutButtonOnClick()
    {
        var itemToggle = _toggleGroup.GetFirstActiveToggle().GetComponent<WareHouseToggle>();
        var slider = _description.transform.Find("Slider").GetComponent<Slider>();
        var item = itemToggle.GetComponent<WareHouseToggle>().item;
        int count = Mathf.FloorToInt(slider.value);

        if (itemToggle.IsStore)
        {
            WareHouseMgr.Instance.Store(item.itemInfo.ID, count);
        }
        else
        {
            WareHouseMgr.Instance.TakeOut(item.itemInfo.ID, count);
        }
        UpdateContent(_inventoryContent, InventoryMgr.Instance.GetInventoryList(), _description);
        UpdateContent(_warehouseContent, WareHouseMgr.Instance.GetWareHouseList(), _description);
    }

    private void SortContent(GameObject content)
    {
        var childs = content.GetAllChilds();
        int i = 0;
        foreach (var info in MaterialTable.Instance.GetDictionary().Values)
        {
            if (childs.Exists(t => t.name == info.Name))
            {
                childs.Find(t => t.name == info.Name).transform.SetSiblingIndex(i);
                i++;
            }
        }
    }
}
