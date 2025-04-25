using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class AbilityEffect : Entity
    {
        public bool Enable { get; set; }
        public Entity OwnerAbility => Parent;
        public CombatEntity OwnerEntity => (OwnerAbility as IAbilityEntity).OwnerEntity;
        public Effect EffectConfig { get; set; }
        /// <summary>
        /// 能力Effect执行委托
        /// </summary>
        public Action<CombatEntity> EffectMethod { get; set; }

        public override void Awake(object initData)
        {
            base.Awake(initData);

            EffectConfig = initData as Effect;

            switch(EffectConfig.EffectType)
            {
                case EffectType.造成伤害:AddComponent<DamageEffectComponent>(); break;
            }
        }

        public void TryApplyEffectToTarget(CombatEntity target)
        {
            EffectMethod?.Invoke(target);
        }
    }
}

