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
        /// ���ڼ��������
        /// </summary>
        public WeaponAbility ActiveMainCannon { get; set; }

        /// <summary>
        /// װ��������
        /// </summary>
        public WeaponAbility[] EquipedMainCannon { get; set; } = new WeaponAbility[4];

        /// <summary>
        /// װ���ĸ���
        /// </summary>
        public List<WeaponAbility> EquipedSubCannon { get; set; } = new List<WeaponAbility>();

        /// <summary>
        /// ʵ��λ��
        /// </summary>
        public Vector3 Position { get { return EntityTransform.position; } }

        public Transform EntityTransform { get; set; }

        public MoveComponent MoveComponent { get; set; }

        public ShipAnimationComponent AnimationComponent { get; set; }

        /// <summary>
        /// ����Ŀ��
        /// </summary>
        public CombatEntity Target { get; set; }

        /// <summary>
        /// Ŀ��ʵ�壬��NPCʹ��
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
        /// ��������
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
        /// ������������
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
        /// ���ظ�������
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
        /// �����յ��˺�
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="damageType"></param>
        public void AcceptDamage(int damage,DamageType damageType)
        {
            var shipAttribute = ShipData.ShipAttribute;
            var shieldValue = shipAttribute.curShieldPoint;
            var shieldValueInt = shieldValue.GetValueInt();
            var hullValue = shipAttribute.curHullPoint;
            //δ�ƶ�
            if (shieldValueInt > damage)
            {
                shieldValue.SubValue(damage);
            }
            //�ƶ�
            else
            {
                damage -= shieldValueInt;
                shieldValue.SetValue(0);
                hullValue.SubValue(damage);
            }
        }
    }
}

