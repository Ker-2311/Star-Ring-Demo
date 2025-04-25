using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class AbilityEffectComponent : ECSComponent
    {
        public List<AbilityEffect> AbilityEffects { get; private set; } = new List<AbilityEffect>();

        public override void Awake(object initData)
        {
            if (initData == null)
            {
                return;
            }
            var effects = initData as List<Effect>;
            foreach (var item in effects)
            {
                //Log.Debug($"AbilityEffectComponent Setup {item}");
                var abilityEffect = Entity.AddChild<AbilityEffect>(item);
                AddEffect(abilityEffect);
            }
        }

        public void AddEffect(AbilityEffect abilityEffect)
        {
            AbilityEffects.Add(abilityEffect);
        }

        public void ApplyAllEffectsToTarget(CombatEntity target)
        {
            foreach(var effect in AbilityEffects)
            {
                effect.TryApplyEffectToTarget(target);
            }
        }

    }
}

