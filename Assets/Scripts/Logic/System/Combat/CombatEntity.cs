using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS.Combat
{
    public class CombatEntity : Entity
    {
        /// <summary>
        /// 正在激活的主炮
        /// </summary>
        public WeaponAbility ActiveMainCannon { get; set; }

        /// <summary>
        /// 装备的主炮
        /// </summary>
        public WeaponAbility[] EquipedMainCannon { get; set; } = new WeaponAbility[4];

        /// <summary>
        /// 装备的副炮
        /// </summary>
        public List<WeaponAbility> EquipedSubCannon { get; set; } = new List<WeaponAbility>();

        /// <summary>
        /// 实体位置
        /// </summary>
        public Vector3 Position { get { return EntityTransform.position; } }

        public Transform EntityTransform { get; set; }

        public MoveComponent MoveComponent { get; set; }

        public ShipAnimationComponent AnimationComponent { get; set; }

        /// <summary>
        /// 锁定目标
        /// </summary>
        public CombatEntity Target { get; set; }

        /// <summary>
        /// 目标实体，仅NPC使用
        /// </summary>
        public CombatEntity TargetEntity { get; set; }

        public IShipData ShipData { get; set; }

        public Rigidbody2D Rigidbody { get; set; }

        public override void Awake()
        {
            base.Awake();

            MoveComponent = AddComponent<MoveComponent>();

        }

        /// <summary>
        /// 挂载能力
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configObject"></param>
        /// <returns></returns>
        private T AttachAbility<T>(object configObject) where T : Entity, IAbilityEntity
        {
            var ability = this.AddChild<T>(configObject);
            return ability;
        }

        /// <summary>
        /// 挂载主炮能力
        /// </summary>
        /// <param name="config"></param>
        /// <param name="weaponObject"></param>
        /// <returns></returns>
        public WeaponAbility AttachMainCannonAbility(WeaponConfigObject config,GameObject weaponObject,int index)
        {
            var ability = AttachAbility<WeaponAbility>(config);
            EquipedMainCannon[index] = ability;
            ability.WeaponObject = weaponObject;
            for(int i = 0; i < 4; i++)
            {
                if (EquipedMainCannon[i] != null)
                {
                    ActiveMainCannon = EquipedMainCannon[i];
                    break;
                }
            }
            return ability;
        }

        /// <summary>
        /// 挂载副炮能力
        /// </summary>
        /// <param name="config"></param>
        /// <param name="weaponObject"></param>
        /// <returns></returns>
        public WeaponAbility AttachSubCannonAbility(Weapon config, GameObject weaponObject)
        {
            var ability = AttachAbility<WeaponAbility>(config);
            ability.AddComponent<AutoFireComponent>();
            EquipedSubCannon.Add(ability);
            ability.WeaponObject = weaponObject;
            return ability;
        }

        /// <summary>
        /// 舰船收到伤害
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="damageType"></param>
        public void AcceptDamage(int damage,DamageType damageType)
        {
            var shipAttribute = ShipData.ShipAttribute;
            var shieldValue = shipAttribute.curShieldPoint;
            var shieldValueInt = shieldValue.GetValueInt();
            var hullValue = shipAttribute.curHullPoint;
            //未破盾
            if (shieldValueInt > damage)
            {
                shieldValue.SubValue(damage);
            }
            //破盾
            else
            {
                damage -= shieldValueInt;
                shieldValue.SetValue(0);
                hullValue.SubValue(damage);
            }
        }
    }
}

