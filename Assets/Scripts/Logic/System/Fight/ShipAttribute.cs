using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 舰船属性
/// </summary>
public class ShipAttribute
{
    //当前船体值
    public AttributeValue curHullPoint;
    //最大船体值
    public AttributeValue maxHullPoint;
    //当前护盾值
    public AttributeValue curShieldPoint;
    //最大护盾值
    public AttributeValue maxShieldPoint;
    //当前灵能值
    public AttributeValue curPsionicPoint;
    //最大灵能值
    public AttributeValue maxPsionicPoint;
    //舰船护甲值
    public AttributeValue ArmourPoint;
    //最大舰船速度
    public AttributeValue maxShipSpeed;
    //当前舰船速度
    public AttributeValue curShipSpeed;
    //舰船转向速度
    public AttributeValue ShipRotateSpeed;
    //舰船加速度
    public AttributeValue shipAcceleration;

    public ShipAttribute(ShipInfo info)
    {
        maxHullPoint = new AttributeValue();
        curHullPoint = new AttributeValue();
        maxShieldPoint = new AttributeValue();
        curShieldPoint = new AttributeValue();
        ArmourPoint = new AttributeValue();
        maxShipSpeed = new AttributeValue();
        shipAcceleration = new AttributeValue();
        ShipRotateSpeed = new AttributeValue();
        curShipSpeed = new AttributeValue();

        maxHullPoint.SetValue(info.BaseHullPoint);
        curHullPoint.SetValue(info.BaseHullPoint);
        maxShieldPoint.SetValue(info.BaseShieldPoint);
        curShieldPoint.SetValue(info.BaseShieldPoint);
        ArmourPoint.SetValue(info.BaseArmourPoint);
        maxShipSpeed.SetValue(info.MaxSpeed);
        shipAcceleration.SetValue(info.Accelerate);
        ShipRotateSpeed.SetValue(info.RotateSpeed);
        curShipSpeed.SetValue(0);
    }

}
