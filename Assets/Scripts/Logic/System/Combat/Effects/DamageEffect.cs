using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    [Effect(EffectType.造成伤害)]
    public class DamageEffect : Effect
    {
        [ToggleGroup("Enabled")]
        [LabelText("伤害"), MinValue(0)]
        public int Damage;

        [ToggleGroup("Enabled")]
        public DamageType DamageType;
    }

    [LabelText("伤害类型")]
    public enum DamageType
    {
        [LabelText("物理伤害")]
        Physic = 0,
        [LabelText("魔法伤害")]
        Magic = 1,
        [LabelText("真实伤害")]
        Real = 2,
    }
}

