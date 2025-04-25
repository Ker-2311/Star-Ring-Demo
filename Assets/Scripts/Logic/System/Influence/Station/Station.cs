using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空间站信息
/// </summary>
public class Station
{
    public string StarID;//星系ID
    public string StationID;//空间站ID
    /// <summary>
    /// Key是格子名，Value是建筑ID
    /// </summary>
    public Dictionary<string,Building> Buildings = new Dictionary<string, Building>();//空间站建筑列表

    public Station(string starID)
    {
        StarID = starID;
        //随机生成一个空间站ID
        StationID = IDFactory.GenerateIdFormTime();
    }
       
}
