using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ����������InventoryData
/// </summary>
public class InventoryMgr : Singleton<InventoryMgr>
{
    private Dictionary<string, Item> InventoryData
    {
        get { return DataMgr.Instance.PlayerData.InventoryData; }
        set { DataMgr.Instance.PlayerData.InventoryData = value; }
    }
    /// <summary>
    /// ���ڵ�һ�ο�ʼ��Ϸʱ���ó�ʼ��
    /// </summary>
    public void Init()
    {
       
    }
    /// <summary>
    /// ͨ��ID�ж���Ʒ�Ƿ��ڿ����
    /// </summary>
    /// <param name="id">��ƷID</param>
    /// <param name="type">��Ʒ�ֵ���</param>
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
    /// ���һ����Ʒ�����
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
    /// ɾ������ڵ�һ����Ʒ
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
    /// ������Ʒ��������������û�и���Ʒ����Ӹ���Ʒ�����
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
    /// ������Ʒ����������ɹ��򷵻�true,���򷵻�false
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
    /// �Կ���ֵ䰴��ƷID�Ӵ�С�������򣬷���һ���������Key�б�ÿ�δ򿪿��������һ�ε���
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
