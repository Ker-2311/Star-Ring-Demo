using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    //解析值
    public float ParserValue { get; set; }
    public bool IsLock { get; set; }
    //装备类型
    public EquipmentType EquipmentType { get; set; }
    //装备是否制造
    public bool IsProduce { get; set; }
}
public enum EquipmentType
{
    武器,
    护盾
}