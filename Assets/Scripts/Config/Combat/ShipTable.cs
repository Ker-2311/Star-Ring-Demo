using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipInfo : BaseInfo
{
    [LabelText("名字")]
    public string Name;//武器名称
    [LabelText("舰船类型")]
    public string Type;//武器类型
    [LabelText("描述"), TextArea]
    public string Description;//描述
    [LabelText("是否是玩家舰船")]
    public bool IsPlayerShip;
    [LabelText("基础船体值")]
    public int BaseHullPoint;
    [LabelText("基础护甲值")]
    public int BaseArmourPoint;
    [LabelText("基础护盾值")]
    public int BaseShieldPoint;
    [LabelText("加速度")]
    public float Accelerate;
    [LabelText("最大速度")]
    public float MaxSpeed;
    [LabelText("旋转速度")]
    public float RotateSpeed;
}

public class ShipTable : ConfigTable<ShipInfo, ShipTable>
{
    private Dictionary<string, string> _nameToID = new Dictionary<string, string>();

    public ShipTable()
    {
        Load(ConfigPath.ShipTablePath);
        foreach (var info in this.GetDictionary().Values)
        {
            _nameToID.Add(info.Name, info.ID);
        }
    }

    public ShipInfo GetFromName(string name)
    {
        _nameToID.TryGetValue(name, out var id);
        return this[id];
    }
}
