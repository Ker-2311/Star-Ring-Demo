using ECS.Combat;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class WeaponConfigObject : SerializedScriptableObject
{
    [LabelText("武器信息"), ReadOnly]
    public WeaponInfo Info;

    [LabelText("子弹预制体"), ReadOnly]
    public GameObject BulletPrefab;

    [LabelText("武器预制体"), ReadOnly]
    public GameObject WeaponPrefab;

    [LabelText("武器效果列表")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, ShowItemCount = false, HideAddButton = true)]
    [HideReferenceObjectPicker]
    public List<Effect> Effects = new List<Effect>();

    [HideLabel, OnValueChanged("AddEffect",InvokeOnUndoRedo = false), ValueDropdown("EffectTypeSelect")]
    public string EffectTypeName = "(添加效果)";

    public void AddEffect()
    {
        if (EffectTypeName != "(添加效果)")
        {
            var effectType = typeof(Effect).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(Effect).IsAssignableFrom(x))
                .Where(x => x.GetCustomAttribute<EffectAttribute>() != null)
                .Where(x => x.GetCustomAttribute<EffectAttribute>().EffectType == EffectTypeName)
                .FirstOrDefault();
            var effect = Activator.CreateInstance(effectType) as Effect;
            effect.Enabled = true;
            effect.Label = EffectTypeName;
            Effects.Add(effect);
            EffectTypeName = "(添加效果)";
        }
    }

    public IEnumerable<string> EffectTypeSelect()
    {
        List<string> list = new List<string>();//定义一个list
        foreach (var name in Enum.GetNames(typeof(EffectType)))
        {
            list.Add(name);
        }

        list.Insert(0, "(添加效果)");
        return list;
    }

}
