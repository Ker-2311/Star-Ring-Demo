using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exterior;
using UnityEngine.U2D;
using System;

public class BuildingPanel : BasePanel
{
    //�����İ�ťͼ��
    public Sprite unlockButtonSprite;
    //ԭ������
    private Vector2 _originPosition;
    //����Ԥ����
    private GameObject _gridPrefab;
    //�Ѿ����ɵĸ����б�
    private List<GameObject> _generatedGrid = new List<GameObject>();
    private int _generatedMaxCircleCount = 0;
    //�������ھ�
    private float _innerDiameter;
    //�������
    private GameObject _net;
    //���ܰ�ť��
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

            //��ȡBuilding���ݸ���BuildingButton��ť
            foreach (var building in buildings)
            {
                //���������ɵĸ���
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
    /// �������ܰ�ť
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
    ///  �ֿⰴť������
    /// </summary>
    private void WarehouseButtonOnClick()
    {
        if (PanelMgr.Instance.Peek().name != "WareHousePanel")
        {
            PanelMgr.Instance.Push("Prefabs/UI/Basic/Influence/StationFunction/WareHouse/WareHousePanel");
        }
    }
    /// <summary>
    /// ���ɵ�һ�����ӣ���_buildingGridΪ��ʱ����
    /// </summary>
    private GameObject GenerateFirstGrid()
    {
        return GenerateGrid(_originPosition, 1, 1);
    }
    /// <summary>
    /// ����circleȦ����,�����뽨�����������ʱ����
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
    /// ������һȦ����,����������ʱ����
    /// </summary>
    private void GenerateNextCircle()
    {
        TraverseGenerateGrid(_generatedGrid.Count);
    }
    /// <summary>
    /// ���ɵ�circle+1Ȧ�ĸ���
    /// </summary>
    /// <param name="VectorSum"></param>
    /// <param name="circle"></param>
    /// <returns></returns>
    private void TraverseGenerateGrid(int circle)
    {
        //i�ǵ�ǰ����������ţ�j��ÿ��ƫ�Ƽ���ִ�д�����k�ǵ�ǰƫ�Ƽ�������
        int i = 1, j = 1, k = 1;
        Vector2 GeneratePosition = _originPosition + new Vector2(0, 2 * circle * _innerDiameter);
        while (i <= circle * 6)
        {
            GenerateGrid(GeneratePosition, circle + 1, i);
            GeneratePosition += CalculateOffset(k, _innerDiameter);
            //�ж��Ƿ�Ҫ�仯ƫ�Ƽ���
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
    /// ����������ƫ��
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
    /// ���ɸ���
    /// </summary>
    /// <returns></returns>
    private GameObject GenerateGrid(Vector2 position, int circle, int index)
    {
        GameObject _grid = ResMgr.Instance.GetInstance(_gridPrefab, _net.transform);
        _grid.name = circle + "/" + index;
        _grid.GetComponent<BuildingButton>().Initialize(null);
        _grid.GetComponent<RectTransform>().anchoredPosition = position;
        //������ɵĸ����������ɵĸ����ڣ�ɾ���ø���
        if (_generatedGrid.Find(t => t == _grid))
        {
            GameObject.Destroy(_grid);
            return null;
        }
        else
        {
            //������ɵĸ���Ȧ�����������ɵ����Ȧ���������Ȧ������
            if (circle > _generatedMaxCircleCount)
            {
                _generatedMaxCircleCount = circle;
            }
            _generatedGrid.Add(_grid);
            return _grid;
        }
    }
}









