using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    [Effect(EffectType.����˺�)]
    public class DamageEffect : Effect
    {
        [ToggleGroup("Enabled")]
        [LabelText("�˺�"), MinValue(0)]
        public int Damage;

        [ToggleGroup("Enabled")]
        public DamageType DamageType;
    }

    [LabelText("�˺�����")]
    public enum DamageType
    {
        [LabelText("�����˺�")]
        Physic = 0,
        [LabelText("ħ���˺�")]
        Magic = 1,
        [LabelText("��ʵ�˺�")]
        Real = 2,
    }
}

