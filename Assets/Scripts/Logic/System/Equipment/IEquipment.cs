using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    //����ֵ
    public float ParserValue { get; set; }
    public bool IsLock { get; set; }
    //װ������
    public EquipmentType EquipmentType { get; set; }
    //װ���Ƿ�����
    public bool IsProduce { get; set; }
}
public enum EquipmentType
{
    ����,
    ����
}