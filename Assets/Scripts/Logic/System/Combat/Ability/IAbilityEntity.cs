using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ECS.Combat
{
    /// <summary>
    /// ����ʵ�壬�洢��ĳ�����������������������ܣ������ݺ�״̬
    /// </summary>
    public interface IAbilityEntity
    {
        public CombatEntity OwnerEntity { get; set; }
        public CombatEntity ParentEntity { get; }
        public bool Enable { get; set; }


        /// ���Լ�������
        public void TryActivateAbility();

        /// ��������
        public void ActivateAbility();

        /// ��������
        public void DeactivateAbility();

        /// ��������
        public void EndAbility();

        /// ����
        public void TryFireBullet();
    }
}
