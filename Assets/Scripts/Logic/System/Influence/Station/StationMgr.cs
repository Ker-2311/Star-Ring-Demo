using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMgr : Singleton<StationMgr>
{
    private Dictionary<string, Station> StationData
    {
        get { return DataMgr.Instance.PlayerData.StationData; }
        set { DataMgr.Instance.PlayerData.StationData = value; }
    }

    //当前进入的空间站
    private Station curStation;
    public void Init()
    {

    }

    /// <summary>
    /// 建造空间站
    /// </summary>
    /// <param name="galaxyID"></param>
    public void BuildStation(string galaxyId)
    {
        var station = new Station(galaxyId) { };
        station.Buildings = new Dictionary<string, Building>();
        //在第一格建造空间站核心
        BuildingMgr.Instance.StartBuildConstruct(station, "90019", "1/1");
        StationData.Add(station.StationID, station);


    }

    /// <summary>
    /// 进入一个空间站
    /// </summary>
    /// <param name="stationID"></param>
    /// <returns></returns>
    public bool EnterStation(string stationID)
    {
        Station station;
        if (StationData.TryGetValue(stationID,out station))
        {
            curStation = station;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 销毁空间站
    /// </summary>
    /// <param name="stationID"></param>
    public void DestroyStation(string stationID)
    {

    }

    /// <summary>
    /// 获取所有空间站信息
    /// </summary>
    /// <returns></returns>
    public List<Station> GetAllStation()
    {
        List<Station> stations = new List<Station>();
        foreach (var staion in StationData.Values)
        {
            stations.Add(staion);
        }
        stations.Sort((x, y) => Convert.ToInt32(x.StationID).CompareTo(Convert.ToInt32(y.StationID)));
        return stations;
    }

    public Dictionary<string, Station> GetStationData()
    {
        var dic = new Dictionary<string, Station>();
        foreach (var data in StationData)
        {
            dic.Add(data.Key, data.Value);
        }
        return dic;
        
    }

    public Station GetCurStation()
    {
        return curStation;
    }
}
