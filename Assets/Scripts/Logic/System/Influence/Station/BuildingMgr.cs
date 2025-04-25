using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMgr : Singleton<BuildingMgr>
{
    private List<string> BuildingLockData
    {
        get { return DataMgr.Instance.PlayerData.BuildingLockData; }
        set { DataMgr.Instance.PlayerData.BuildingLockData = value; }
    }

    //所有建筑图标
    private Sprite[] _icons;
    //建筑中图标
    private Sprite _buildingIcon;
    /// <summary>
    /// 建筑效果，Key是效果名，Value是效果回调
    /// </summary>
    private Dictionary<string, Action<object[]>> buildingEffectDic = new Dictionary<string, Action<object[]>>();
    /// <summary>
    /// 仅在第一次进入游戏时调用
    /// </summary>
    public void Init()
    {
        _icons = ResMgr.Instance.GetAllResources<Sprite>("Image/Icon/Building");
        _buildingIcon = ResMgr.Instance.GetResource<Sprite>("Image/Icon/Building/建筑中图标");
        foreach (var buildingType in BuildingTable.Instance.GetDictionary().Values)
        {
            BuildingLockData.Add(buildingType.ID);
        }
        Type buildingEffect = typeof(BuildingEffect);
        var methods = buildingEffect.GetMethods();
        foreach (var effect in methods)
        {
            if (effect.ReturnType == typeof(void))
            {
                buildingEffectDic.Add(effect.Name, (object[] par) => effect.Invoke(null, par));
            }
        }
        //将空间站核心添加到可建筑列表里
        //Data.instance.BuildingLockData["90019"].IsLock = false;
    }

    /// <summary>
    /// 返回一个可建筑列表
    /// </summary>
    /// <returns></returns>
    public List<string> GetBuildableList()
    {
        return BuildingLockData;
    }

    /// <summary>
    /// 开始建造建筑
    /// </summary>
    /// <param name="buildingID"></param>
    /// <param name="gridName"></param>
    public Building StartBuildConstruct(Station station,string buildingID,string gridName)
    {
        foreach (var icon in _icons)
        {
            if (icon.name == (BuildingTable.Instance[buildingID].Name+"1级"))
            {
                var building = new Building(buildingID, icon);
                //更新数据
                station.Buildings.Add(gridName, building);
                //添加建造中时间流
                GameTimeMgr.Instance.AddTimeStream(building.buildingInfo.BuildingTime, 1, () => building.buildingRemainTime--);
                GameTimeMgr.Instance.AddTimeStream(1, building.buildingInfo.BuildingTime,() => FinishBuilding(building));
                return building;
            }
        }

        return null;
    }

    /// <summary>
    /// 完成建造
    /// </summary>
    /// <param name="building"></param>
    private void FinishBuilding(Building building)
    {
        RunBuildingEffect(building);
    }

    /// <summary>
    /// 查询是否存在建筑
    /// </summary>
    /// <param name="gridName"></param>
    public bool IsBuildingExist(string stationID,string buildingID)
    {
        var stationData = StationMgr.Instance.GetStationData();
        if (stationData.ContainsKey(stationID))
        {
            foreach (var building in stationData[stationID].Buildings.Values)
            {
                if (building.buildingInfo.ID == buildingID)
                {
                    return true;
                }
            }
            
        }
        return false;
    }

    public void Test()
    {
        var a = BuildingTable.Instance["57121"];
        buildingEffectDic["SourcesAndMaterialMouthChange"](new object[] { a.MouthSourcesCost, a.MouthSourcesOutput, a.MouthMaterialOutput });
    }

    /// <summary>
    /// 获得建筑中图标
    /// </summary>
    /// <returns></returns>
    public Sprite GetBuildingIcon()
    {
        return _buildingIcon;
    }

    /// <summary>
    /// 执行建筑效果函数
    /// </summary>
    private void RunBuildingEffect(Building building,params object[] par)
    {
        if (building.buildingInfo.BuildingEffectType != null && buildingEffectDic.ContainsKey(building.buildingInfo.BuildingEffectType))
        {
            buildingEffectDic[building.buildingInfo.BuildingEffectType](par);
        }
    }
}