using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class EffectAttribute : Attribute
    {
        readonly EffectType effectType;

        public EffectAttribute(EffectType effectType)
        {
            this.effectType = effectType;
        }

        public string EffectType
        {
            get { return effectType.ToString(); }
        }
    }

    public enum EffectType
    {
        ‘Ï≥……À∫¶,
    }

    [Serializable]
    public abstract class Effect
    {
        [HideInInspector]
        public string Label;

        [ToggleGroup("Enabled", "$Label")]
        public bool Enabled;

        [HideInInspector]
        public EffectType EffectType;
    }
}

