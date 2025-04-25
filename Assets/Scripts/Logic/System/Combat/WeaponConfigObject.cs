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
    [LabelText("������Ϣ"), ReadOnly]
    public WeaponInfo Info;

    [LabelText("�ӵ�Ԥ����"), ReadOnly]
    public GameObject BulletPrefab;

    [LabelText("����Ԥ����"), ReadOnly]
    public GameObject WeaponPrefab;

    [LabelText("����Ч���б�")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, ShowItemCount = false, HideAddButton = true)]
    [HideReferenceObjectPicker]
    public List<Effect> Effects = new List<Effect>();

    [HideLabel, OnValueChanged("AddEffect",InvokeOnUndoRedo = false), ValueDropdown("EffectTypeSelect")]
    public string EffectTypeName = "(���Ч��)";

    public void AddEffect()
    {
        if (EffectTypeName != "(���Ч��)")
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
            EffectTypeName = "(���Ч��)";
        }
    }

    public IEnumerable<string> EffectTypeSelect()
    {
        List<string> list = new List<string>();//����һ��list
        foreach (var name in Enum.GetNames(typeof(EffectType)))
        {
            list.Add(name);
        }

        list.Insert(0, "(���Ч��)");
        return list;
    }

}
