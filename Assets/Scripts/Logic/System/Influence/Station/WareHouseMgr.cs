using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseMgr : Singleton<WareHouseMgr>
{
    private Dictionary<string, Item> InventoryData
    {
        get { return DataMgr.Instance.PlayerData.InventoryData; }
        set { DataMgr.Instance.PlayerData.InventoryData = value; }
    }

    private Dictionary<string, Item> WareHouseData
    {
        get { return DataMgr.Instance.PlayerData.WareHouseData; }
        set { DataMgr.Instance.PlayerData.WareHouseData = value; }
    }

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool Store(string itemID,int count)
    {
        //如果库存里存在该物品并且大于要存储的数量
        if (InventoryData.ContainsKey(itemID))
        {
            if (InventoryData[itemID].count >= count)
            {
                //遍历仓库如果物品已存在则增加数量
                foreach (var i in WareHouseData)
                {
                    if (i.Key == itemID)
                    {
                        WareHouseData[itemID].count += count;
                        InventoryData[itemID].count -= count;
                        if (InventoryData[itemID].count == 0)
                        {
                            InventoryData.Remove(itemID);
                        }
                        return true;
                    }
                }
                WareHouseData.Add(itemID, new Item(itemID) { count = count });
                InventoryData[itemID].count -= count;
                if (InventoryData[itemID].count == 0)
                {
                    InventoryData.Remove(itemID);
                }
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 取出物品
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool TakeOut(string itemID, int count)
    {
        //与存储相反
        if (WareHouseData.ContainsKey(itemID))
        {
            if (WareHouseData[itemID].count >= count)
            {
                foreach (var i in InventoryData)
                {
                    if (i.Key == itemID)
                    {
                        InventoryData[itemID].count += count;
                        WareHouseData[itemID].count -= count;
                        if (WareHouseData[itemID].count == 0)
                        {
                            WareHouseData.Remove(itemID);
                        }
                        return true;
                    }
                }
                InventoryData.Add(itemID, new Item(itemID) { count = count });
                WareHouseData[itemID].count -= count;
                if (WareHouseData[itemID].count == 0)
                {
                    WareHouseData.Remove(itemID);
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取当前库存列表
    /// </summary>
    /// <returns></returns>
    public List<Item> GetWareHouseList()
    {
        var list = new List<Item>();
        foreach (var itemID in MaterialTable.Instance.GetDictionary().Keys)
        {
            if (WareHouseData.ContainsKey(itemID))
            {
                list.Add(WareHouseData[itemID]);
            }
        }
        return list;
    }
}
