using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑类型
/// </summary>
public class Building
{
    public BuildingInfo buildingInfo;
    public int CurRank;
    //建造剩余时间
    public int buildingRemainTime;
    //建筑图标
    public Sprite icon;
    //是否建造完成
    public bool builded = false;
    //public bool IsLock = true;//是否锁定

    public Building(string id,Sprite buildingIcon)
    {
        icon = buildingIcon;
        buildingInfo = BuildingTable.Instance[id];
        buildingRemainTime = buildingInfo.BuildingTime;
        CurRank = 1;
    }
}
