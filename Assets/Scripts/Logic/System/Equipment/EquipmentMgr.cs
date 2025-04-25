using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMgr : Singleton<EquipmentMgr>
{
    private Dictionary<string, IEquipment> EquipmentData
    {
        get { return DataMgr.Instance.PlayerData.EquipmentData; }
        set { DataMgr.Instance.PlayerData.EquipmentData = value; }
    }

    /// <summary>
    /// 获取对应装备列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<IEquipment> GetEquipmentsList(EquipmentType type)
    {
        var list = new List<IEquipment>();
        switch (type)
        {
            case EquipmentType.武器:
                foreach (var info in WeaponTable.Instance.GetDictionary())
                {
                    list.Add(EquipmentData[info.Key]);
                }
                break;
            case EquipmentType.护盾:
                foreach (var info in ShieldTable.Instance.GetDictionary())
                {
                    list.Add(EquipmentData[info.Key]);
                }
                break;
        }
        return list;
    }


    public T GetEquipment<T>(string id) where T:class
    {
        return EquipmentData[id] as T;
    }

    public Dictionary<string, IEquipment> GetEquipmentData()
    {
        var dic = new Dictionary<string, IEquipment>();
        foreach (var data in EquipmentData)
        {
            dic.Add(data.Key, data.Value);
        }
        return dic;
    }
}
