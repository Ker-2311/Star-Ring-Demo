using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class AutoFireComponent : ECSComponent
    {
        public WeaponAbility WeaponAbility { get; set; }

        public CombatEntity CombatEntity { get; set; } 

        public override void Awake()
        {
            base.Awake();

            WeaponAbility = Entity as WeaponAbility;
            CombatEntity = WeaponAbility.OwnerEntity;
        }

        public override void Update()
        {
            base.Update();

            if (CombatEntity.Target != null)
            {
                StartFireAbility(WeaponAbility);
            }
        }

        private void StartFireAbility(WeaponAbility ability)
        {
            if (ability == null) { return; }

            ability.TryFireBullet();
        }
    }
}

