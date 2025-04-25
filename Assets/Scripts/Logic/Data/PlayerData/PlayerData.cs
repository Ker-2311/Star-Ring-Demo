using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData:Data
{
    //星币
    public int MoneyData { get; set; }

    //时间
    public GameTime TimeData { get; set; }

    //库存数据
    public Dictionary<string, Item> InventoryData { get; set; }
    //仓库数据
    public Dictionary<string, Item> WareHouseData { get; set; }
    //资源数据
    public Dictionary<string, Source> SourcesData { get; set; }
    // 科技数据,存放玩家已解锁和正在研究的科技
    public Dictionary<string, Science> ScienceData { get; set; }
    //星系数据,存放所有星系
    public Dictionary<string, Star> StarData { get; set; }

    //建筑解锁数据
    public List<string> BuildingLockData { get; set; }
    //空间站数据, 包括已建立的空间站信息
    public Dictionary<string, Station> StationData { get; set; }
    //势力数据
    public Dictionary<string, Force> ForceData { get; set; }

    //装备数据
    public Dictionary<string, IEquipment> EquipmentData { get; set; } 

    /// <summary>
    /// 存放玩家所有舰船数据
    /// </summary>
    public List<PlayerShip> PlayerShipsData { get; set; }

    //玩家正在激活的舰船
    public PlayerShip ActivePlayerShip { get; set; }

    public Dictionary<string,NPCShip> NPCShipsData { get; set; }
}
