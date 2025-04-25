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

    //���н���ͼ��
    private Sprite[] _icons;
    //������ͼ��
    private Sprite _buildingIcon;
    /// <summary>
    /// ����Ч����Key��Ч������Value��Ч���ص�
    /// </summary>
    private Dictionary<string, Action<object[]>> buildingEffectDic = new Dictionary<string, Action<object[]>>();
    /// <summary>
    /// ���ڵ�һ�ν�����Ϸʱ����
    /// </summary>
    public void Init()
    {
        _icons = ResMgr.Instance.GetAllResources<Sprite>("Image/Icon/Building");
        _buildingIcon = ResMgr.Instance.GetResource<Sprite>("Image/Icon/Building/������ͼ��");
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
        //���ռ�վ������ӵ��ɽ����б���
        //Data.instance.BuildingLockData["90019"].IsLock = false;
    }

    /// <summary>
    /// ����һ���ɽ����б�
    /// </summary>
    /// <returns></returns>
    public List<string> GetBuildableList()
    {
        return BuildingLockData;
    }

    /// <summary>
    /// ��ʼ���콨��
    /// </summary>
    /// <param name="buildingID"></param>
    /// <param name="gridName"></param>
    public Building StartBuildConstruct(Station station,string buildingID,string gridName)
    {
        foreach (var icon in _icons)
        {
            if (icon.name == (BuildingTable.Instance[buildingID].Name+"1��"))
            {
                var building = new Building(buildingID, icon);
                //��������
                station.Buildings.Add(gridName, building);
                //��ӽ�����ʱ����
                GameTimeMgr.Instance.AddTimeStream(building.buildingInfo.BuildingTime, 1, () => building.buildingRemainTime--);
                GameTimeMgr.Instance.AddTimeStream(1, building.buildingInfo.BuildingTime,() => FinishBuilding(building));
                return building;
            }
        }

        return null;
    }

    /// <summary>
    /// ��ɽ���
    /// </summary>
    /// <param name="building"></param>
    private void FinishBuilding(Building building)
    {
        RunBuildingEffect(building);
    }

    /// <summary>
    /// ��ѯ�Ƿ���ڽ���
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
    /// ��ý�����ͼ��
    /// </summary>
    /// <returns></returns>
    public Sprite GetBuildingIcon()
    {
        return _buildingIcon;
    }

    /// <summary>
    /// ִ�н���Ч������
    /// </summary>
    private void RunBuildingEffect(Building building,params object[] par)
    {
        if (building.buildingInfo.BuildingEffectType != null && buildingEffectDic.ContainsKey(building.buildingInfo.BuildingEffectType))
        {
            buildingEffectDic[building.buildingInfo.BuildingEffectType](par);
        }
    }
}