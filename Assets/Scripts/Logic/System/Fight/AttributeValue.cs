using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理舰船属性的值
/// </summary>
public class AttributeValue
{
    private float _value = 0;
    private List<AttributeModify> _modifies = new List<AttributeModify>();

    public void SetValue(float value)
    {
        _value = value;
    }

    public void AddValue(float value)
    {
        _value += value;
    }

    public void SubValue(float value)
    {
        _value -= value;
    }

    public float GetValueFloat()
    {
        float value = _value;
        foreach (var modify in _modifies)
        {
            value = modify.ApplyModify(value);
        }
        return value;
    }

    public int GetValueInt()
    {
        return (int)GetValueFloat();
    }

}
