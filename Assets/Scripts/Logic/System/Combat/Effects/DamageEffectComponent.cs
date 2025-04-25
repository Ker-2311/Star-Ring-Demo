using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Combat
{
    public class DamageEffectComponent:ECSComponent
    {
        public AbilityEffect AbilityEffect => (Entity as AbilityEffect);
        public DamageEffect DamageEffect { get; set; }

        public override void Awake()
        {
            base.Awake();
            DamageEffect = AbilityEffect.EffectConfig as DamageEffect;
            AbilityEffect.EffectMethod = TriggerEffect;
        }

        public void TriggerEffect(CombatEntity target)
        {
            if (target == null) return;
            target.AcceptDamage(DamageEffect.Damage, DamageEffect.DamageType);
        }
    }
}
