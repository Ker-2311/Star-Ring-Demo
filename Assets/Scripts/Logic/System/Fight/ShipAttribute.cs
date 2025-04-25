using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class ShipAttribute
{
    //��ǰ����ֵ
    public AttributeValue curHullPoint;
    //�����ֵ
    public AttributeValue maxHullPoint;
    //��ǰ����ֵ
    public AttributeValue curShieldPoint;
    //��󻤶�ֵ
    public AttributeValue maxShieldPoint;
    //��ǰ����ֵ
    public AttributeValue curPsionicPoint;
    //�������ֵ
    public AttributeValue maxPsionicPoint;
    //��������ֵ
    public AttributeValue ArmourPoint;
    //��󽢴��ٶ�
    public AttributeValue maxShipSpeed;
    //��ǰ�����ٶ�
    public AttributeValue curShipSpeed;
    //����ת���ٶ�
    public AttributeValue ShipRotateSpeed;
    //�������ٶ�
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
