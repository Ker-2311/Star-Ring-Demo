using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipInfo : BaseInfo
{
    [LabelText("����")]
    public string Name;//��������
    [LabelText("��������")]
    public string Type;//��������
    [LabelText("����"), TextArea]
    public string Description;//����
    [LabelText("�Ƿ�����ҽ���")]
    public bool IsPlayerShip;
    [LabelText("��������ֵ")]
    public int BaseHullPoint;
    [LabelText("��������ֵ")]
    public int BaseArmourPoint;
    [LabelText("��������ֵ")]
    public int BaseShieldPoint;
    [LabelText("���ٶ�")]
    public float Accelerate;
    [LabelText("����ٶ�")]
    public float MaxSpeed;
    [LabelText("��ת�ٶ�")]
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
