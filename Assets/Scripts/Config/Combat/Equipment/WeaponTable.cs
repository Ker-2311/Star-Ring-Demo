using ECS.Combat;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// �ӵ��Ŀ���ʽ
/// </summary>
public enum BulletFireType
{
    ����,
    ����,
    ����,
    ����
}

/// <summary>
/// �ӵ��������ͣ������ӵ����˶��켣
/// </summary>
public enum BulletFlightType
{
    �䵯��,
    ������,
    ������,
    �Ż���
}

[Serializable]
public class WeaponInfo:BaseInfo
{
    [LabelText("����")]
    public string Name;//��������
    [LabelText("��������")]
    public string Type;//��������
    [LabelText("����"), TextArea]
    public string Description;//����
    [LabelText("ǰ�ÿƼ�ID")]
    public string FrontTechID;//ǰ�ÿƼ�ID
    [LabelText("������ϻ���"),NonSerialized, OdinSerialize]
    public Dictionary<string, int> MaterialsCost;//������ϻ���
    [LabelText("�����Ǳһ���")]
    public float Cost;//�����Ǳһ���
    [LabelText("ǰ������100%����������")]
    public string FrontWeaponID;//ǰ������100%����������
    [LabelText("������25%����")]
    public string Property25;//������25%����
    [LabelText("������50%����")]
    public string Property50;
    [LabelText("������75%����")]
    public string Property75;
    [LabelText("�����˺�")]
    public float Damage; //�����˺�
    [LabelText("���")]
    public float Range; //���
    [LabelText("�ӵ��ٶ�")]
    public float BulletSpeed; //�ӵ��ٶ�
    [LabelText("��ת�ٶ�")]
    public float RotateSpeed; //��ת�ٶ�
    [LabelText("������ȴ")]
    public float FireCd; //������ȴ
    [LabelText("�ӵ�ɢ��")]
    public float Scatting; //�ӵ�ɢ��
    [LabelText("����ʱ��")]
    public float AccumulateTime; //����ʱ��
    [LabelText("����ʱ��")]
    public float ChannelTime; //����ʱ��
    [LabelText("�ӵ�����")]
    public BulletFlightType BulletFlightType;
    [LabelText("��������")]
    public BulletFireType BulletFireType;
    [ShowInInspector, LabelText("�ӵ�Ч��")]
    public List<Effect> Effects;

    public List<string> ImpactEffectType = new List<string>();//����Ч��,ֻ�ֿ��˲�ͬЧ����������Ҫ��Effect����ȥ����
}

public class WeaponTable : ConfigTable<WeaponInfo,WeaponTable>
{
    public WeaponTable()
    {
        Load(ConfigPath.WeaponTablePath);
    }

    protected override void Paraser(FieldInfo fieldsInfo, string db, WeaponInfo roleData)
    {
        base.Paraser(fieldsInfo, db, roleData);
        if (fieldsInfo.FieldType == typeof(BulletFlightType)) 
        {
            fieldsInfo.SetValue(roleData, Enum.Parse<BulletFlightType>(db)); 
        }
    }
}
