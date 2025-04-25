using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;
using UnityEngine.U2D;
using System;

public class BuildingPanel : BasePanel
{
    //解锁的按钮图标
    public Sprite unlockButtonSprite;
    //原点坐标
    private Vector2 _originPosition;
    //格子预制体
    private GameObject _gridPrefab;
    //已经生成的格子列表
    private List<GameObject> _generatedGrid = new List<GameObject>();
    private int _generatedMaxCircleCount = 0;
    //六边形内径
    private float _innerDiameter;
    //建造面板
    private GameObject _net;
    //功能按钮组
    private GameObject _functionButtonGroup;

    public override void Awake()
    {
        base.OnEnter();
        _net = transform.Find("Scroll View/Viewport/Net").gameObject;
        _gridPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Influence/Building/BuildingButton");
        _originPosition = _net.GetComponent<RectTransform>().anchoredPosition;
        _functionButtonGroup = transform.Find("FunctionBlock/ButtonGroup").gameObject;

        transform.Find("Scroll View").GetComponent<MyScrollRect>().content = _net.GetComponent<RectTransform>();
        var _gridRect = _gridPrefab.GetComponent<RectTransform>();
        _innerDiameter = (0.5f * _gridRect.sizeDelta.y * _gridRect.localScale.x) - 2;

        var stationCore = GenerateFirstGrid();
        var warehouseButton = _functionButtonGroup.transform.Find("WarehouseButton").GetComponent<Button>();

        UnlockFunctionButton(warehouseButton);
        warehouseButton.onClick.AddListener(WarehouseButtonOnClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        var buildings = StationMgr.Instance.GetCurStation().Buildings;
        var stationCore = buildings["1/1"];
        if (stationCore != null)
        {
            GenerateNet(2 + stationCore.CurRank);

            //读取Building数据更新BuildingButton按钮
            foreach (var building in buildings)
            {
                //遍历已生成的格子
                foreach (var grid in _generatedGrid)
                {
                    if (grid.name == building.Key)
                    {
                        var buildingButton = grid.GetComponent<BuildingButton>();
                        buildingButton.StartBuild(building.Value);
                        if (building.Value.builded)
                        {
                            buildingButton.Builded();
                        }
                        else
                        {
                            buildingButton.OnBuilding();
                        }
                        break;
                    }
                }
            }
        }

    }
    /// <summary>
    /// 解锁功能按钮
    /// </summary>
    /// <param name="button"></param>
    private void UnlockFunctionButton(Button button)
    {
        var icon = button.transform.Find("Icon");
        icon.gameObject.SetActive(true);
        button.GetComponent<Image>().sprite = unlockButtonSprite;
        button.interactable = true;
    }
    /// <summary>
    ///  仓库按钮被按下
    /// </summary>
    private void WarehouseButtonOnClick()
    {
        if (PanelMgr.Instance.Peek().name != "WareHousePanel")
        {
            PanelMgr.Instance.Push("Prefabs/UI/Basic/Influence/StationFunction/WareHouse/WareHousePanel");
        }
    }
    /// <summary>
    /// 生成第一个格子，当_buildingGrid为空时调用
    /// </summary>
    private GameObject GenerateFirstGrid()
    {
        return GenerateGrid(_originPosition, 1, 1);
    }
    /// <summary>
    /// 生成circle圈网格,当进入建造界面或建造核心时调用
    /// </summary>
    /// <param name="circle"></param>
    private void GenerateNet(int circle)
    {
        for (int i = 1; i <= (circle - 1); i++)
        {
            TraverseGenerateGrid(i);
        }
    }
    /// <summary>
    /// 生成下一圈格子,当核心升级时调用
    /// </summary>
    private void GenerateNextCircle()
    {
        TraverseGenerateGrid(_generatedGrid.Count);
    }
    /// <summary>
    /// 生成第circle+1圈的格子
    /// </summary>
    /// <param name="VectorSum"></param>
    /// <param name="circle"></param>
    /// <returns></returns>
    private void TraverseGenerateGrid(int circle)
    {
        //i是当前遍历格子序号，j是每种偏移计算执行次数，k是当前偏移计算类型
        int i = 1, j = 1, k = 1;
        Vector2 GeneratePosition = _originPosition + new Vector2(0, 2 * circle * _innerDiameter);
        while (i <= circle * 6)
        {
            GenerateGrid(GeneratePosition, circle + 1, i);
            GeneratePosition += CalculateOffset(k, _innerDiameter);
            //判断是否要变化偏移计算
            if (j == circle)
            {
                j = 1;
                k++;
                i++;
                continue;
            }
            j++;
            i++;
        }
    }
    /// <summary>
    /// 计算六边形偏置
    /// </summary>
    /// <param name="index"></param>
    /// <param name="innerDiameter"></param>
    /// <returns></returns>
    private Vector2 CalculateOffset(int index, float innerDiameter)
    {
        switch (index)
        {
            case 1: return new Vector2(2 * innerDiameter * 0.866025404f, -2 * innerDiameter * 0.5f);
            case 2: return new Vector2(0, -2 * innerDiameter);
            case 3: return new Vector2(-2 * innerDiameter * 0.866025404f, -2 * innerDiameter * 0.5f);
            case 4: return new Vector2(-2 * innerDiameter * 0.866025404f, 2 * innerDiameter * 0.5f);
            case 5: return new Vector2(0, 2 * innerDiameter);
            case 6: return new Vector2(2 * innerDiameter * 0.866025404f, 2 * innerDiameter * 0.5f);
            default: return Vector2.zero;
        }
    }
    /// <summary>
    /// 生成格子
    /// </summary>
    /// <returns></returns>
    private GameObject GenerateGrid(Vector2 position, int circle, int index)
    {
        GameObject _grid = ResMgr.Instance.GetInstance(_gridPrefab, _net.transform);
        _grid.name = circle + "/" + index;
        _grid.GetComponent<BuildingButton>().Initialize(null);
        _grid.GetComponent<RectTransform>().anchoredPosition = position;
        //如果生成的格子在已生成的格子内，删除该格子
        if (_generatedGrid.Find(t => t == _grid))
        {
            GameObject.Destroy(_grid);
            return null;
        }
        else
        {
            //如果生成的格子圈数大于已生成的最大圈数，则最大圈数增加
            if (circle > _generatedMaxCircleCount)
            {
                _generatedMaxCircleCount = circle;
            }
            _generatedGrid.Add(_grid);
            return _grid;
        }
    }
}









