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
/// 子弹的开火方式
/// </summary>
public enum BulletFireType
{
    单发,
    连发,
    蓄力,
    持续
}

/// <summary>
/// 子弹飞行类型，决定子弹的运动轨迹
/// </summary>
public enum BulletFlightType
{
    射弹型,
    激光型,
    导弹型,
    僚机型
}

[Serializable]
public class WeaponInfo:BaseInfo
{
    [LabelText("名字")]
    public string Name;//武器名称
    [LabelText("武器类型")]
    public string Type;//武器类型
    [LabelText("描述"), TextArea]
    public string Description;//描述
    [LabelText("前置科技ID")]
    public string FrontTechID;//前置科技ID
    [LabelText("制造材料花费"),NonSerialized, OdinSerialize]
    public Dictionary<string, int> MaterialsCost;//制造材料花费
    [LabelText("制造星币花费")]
    public float Cost;//制造星币花费
    [LabelText("前置武器100%解析度需求")]
    public string FrontWeaponID;//前置武器100%解析度需求
    [LabelText("解析度25%特性")]
    public string Property25;//解析度25%特性
    [LabelText("解析度50%特性")]
    public string Property50;
    [LabelText("解析度75%特性")]
    public string Property75;
    [LabelText("单发伤害")]
    public float Damage; //单发伤害
    [LabelText("射程")]
    public float Range; //射程
    [LabelText("子弹速度")]
    public float BulletSpeed; //子弹速度
    [LabelText("旋转速度")]
    public float RotateSpeed; //旋转速度
    [LabelText("开火冷却")]
    public float FireCd; //开火冷却
    [LabelText("子弹散射")]
    public float Scatting; //子弹散射
    [LabelText("蓄力时间")]
    public float AccumulateTime; //蓄力时间
    [LabelText("引导时间")]
    public float ChannelTime; //引导时间
    [LabelText("子弹类型")]
    public BulletFlightType BulletFlightType;
    [LabelText("开火类型")]
    public BulletFireType BulletFireType;
    [ShowInInspector, LabelText("子弹效果")]
    public List<Effect> Effects;

    public List<string> ImpactEffectType = new List<string>();//命中效果,只分开了不同效果，具体需要到Effect类中去解析
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
