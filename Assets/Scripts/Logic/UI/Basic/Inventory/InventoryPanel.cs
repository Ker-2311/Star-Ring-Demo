using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;

public class InventoryPanel : BasePanel
{
    private GameObject _content;
    private GameObject _itemListTogglePrefab;
    private GameObject _itemGridTogglePrefab;
    private GameObject _description;
    private GameObject _descriptionText;
    private Sprite[] _icons;
    //是否是格子状态
    private bool _isGridStatus = true;

    public override void Awake()
    {
        base.OnEnter();
        _content = gameObject.FindObject("ScrollRect/Viewport/Content");
        _icons = Resources.LoadAll<Sprite>("Image/Icon/Item");
        _description = gameObject.FindObject("Description");
        _descriptionText = gameObject.FindObject("DescriptionText");
        _itemListTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Inventory/ListToggle");
        _itemGridTogglePrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Inventory/GridToggle");
        var gridButton = transform.Find("GridButton").GetComponent<Button>();
        var listButton = transform.Find("ListButton").GetComponent<Button>();
        gridButton.onClick.AddListener(GridButtonOnClick);
        listButton.onClick.AddListener(ListButtonOnClick);

        IncreaseAllIteam();
        UpdateItemContent(_itemGridTogglePrefab);
    }

    /// <summary>
    /// 测试用，增加所有物品
    /// </summary>
    private void IncreaseAllIteam()
    {
        foreach (var itemId in MaterialTable.Instance.GetDictionary().Keys)
        {
            InventoryMgr.Instance.IncreaseItem(itemId, 50);
        }
    }

    /// <summary>
    /// 更新Item列表
    /// </summary>
    private void UpdateItemContent(GameObject prefab)
    {
        _content.DestroyChilds();
        var itemList = InventoryMgr.Instance.GetInventoryList();
        var toggleGroup = _content.GetComponent<ToggleGroup>();
        for (int i = 0; i < itemList.Count; i++)
        {
            //遍历生成Toggle
            var item = ResMgr.Instance.GetInstance(prefab, _content.transform);
            var toggle = item.GetComponent<Toggle>();

            item.GetComponent<ItemToggle>().Init(itemList[i], _description, _icons);
            toggle.group = toggleGroup;
        }
    }

    private void ListButtonOnClick()
    {
        var layout = _content.GetComponent<GridLayoutGroup>();
        if (_isGridStatus)
        {
            _isGridStatus = false;
            layout.cellSize = _itemListTogglePrefab.GetComponent<RectTransform>().sizeDelta;
            UpdateItemContent(_itemListTogglePrefab);
            _descriptionText.SetActive(true);
        }     
    }

    private void GridButtonOnClick()
    {
        var layout = _content.GetComponent<GridLayoutGroup>();
        if (!_isGridStatus)
        {
            _isGridStatus = true;
            layout.cellSize = _itemGridTogglePrefab.GetComponent<RectTransform>().sizeDelta;
            UpdateItemContent(_itemGridTogglePrefab);
            _descriptionText.SetActive(false);
        }
    }
}
