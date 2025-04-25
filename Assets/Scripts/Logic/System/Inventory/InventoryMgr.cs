using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 管理库存数据InventoryData
/// </summary>
public class InventoryMgr : Singleton<InventoryMgr>
{
    private Dictionary<string, Item> InventoryData
    {
        get { return DataMgr.Instance.PlayerData.InventoryData; }
        set { DataMgr.Instance.PlayerData.InventoryData = value; }
    }
    /// <summary>
    /// 仅在第一次开始游戏时调用初始化
    /// </summary>
    public void Init()
    {
       
    }
    /// <summary>
    /// 通过ID判断物品是否在库存里
    /// </summary>
    /// <param name="id">物品ID</param>
    /// <param name="type">物品字典名</param>
    /// <returns></returns>
    public bool ItemIsExists(string id)
    {
        if (InventoryData.ContainsKey(id))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 添加一个物品进库存
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ItemDictionary.itemDictionary[id].type"></param>
    public void AddItem(string id)
    {
        if (!ItemIsExists(id))
        {
            Item item = new Item(id);
            item.count = 1;
            InventoryData.Add(id, item);
        }
    }
    /// <summary>
    /// 删除库存内的一个物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    public void DeleteItem(string id)
    {
        if (ItemIsExists(id))
        {
            InventoryData.Remove(id);
        }
    }
    /// <summary>
    /// 增加物品数量，如果库存里没有该物品则添加该物品进库存
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void IncreaseItem(string id, int count)
    {
        if (ItemIsExists(id))
        {
            InventoryData[id].count += count;
        }
        else
        {
            AddItem(id);
            InventoryData[id].count = count;
        }
    }
    /// <summary>
    /// 减少物品数量，如果成功则返回true,否则返回false
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool DecreaseItem(string id, int count)
    {
        if (InventoryData[id].count > count)
        {
            InventoryData[id].count -= count;
            return true;
        }
        else if (InventoryData[id].count < count)
        {
            return false;
        }
        else if (InventoryData[id].count == count)
        {
            DeleteItem(id);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 对库存字典按物品ID从大到小进行排序，返回一个排序过的Key列表，每次打开库存界面进行一次调用
    /// </summary>
    public List<Item> GetInventoryList()
    {
        var list = new List<Item>();
        foreach (var itemID in MaterialTable.Instance.GetDictionary().Keys)
        {
            if (InventoryData.ContainsKey(itemID))
            {
                list.Add(InventoryData[itemID]);
            }
        }
        return list;
    }
}
