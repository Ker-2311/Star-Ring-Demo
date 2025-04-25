using ECS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon:IEquipment
{
    public float ParserValue { get; set; }
    public bool IsLock { get; set; }
    public EquipmentType EquipmentType { get; set; }
    public bool IsProduce { get; set; }
    /// <summary>
    /// ��ֵ��Ϣ
    /// </summary>
    public WeaponInfo WeaponInfo { get; set; }
}
