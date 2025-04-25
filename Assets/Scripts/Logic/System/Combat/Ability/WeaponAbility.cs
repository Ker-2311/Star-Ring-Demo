using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public partial class WeaponAbility : Entity, IAbilityEntity
    {
        public CombatEntity OwnerEntity { get { return GetParent<CombatEntity>(); } set { } }
        public CombatEntity ParentEntity { get => GetParent<CombatEntity>(); }
        public bool Enable { get; set; } = false;
        //��������
        public WeaponConfigObject ConfigObject { get; set; }
        public bool IsCold { get { return ColdComponent.ColdCD > 0; } }

        public GameObject WeaponObject { get; set; }
        //Ч�����
        public AbilityEffectComponent AbilityEffectComponent { get; set; }
        //��ȴ���
        public AbilityColdComponent ColdComponent { get; set; }

        //����Ŀ���
        public Vector3 TargetPos { get; set; }

        private WeaponExecution _execution { get; set; }

        public override void Awake(object initData)
        {
            base.Awake(initData);
            ConfigObject = initData as WeaponConfigObject;
            ColdComponent = AddComponent<AbilityColdComponent>();
            AbilityEffectComponent = AddComponent<AbilityEffectComponent>(ConfigObject.Effects);
            Name = ConfigObject.Info.Name;
        }

        public override void Update()
        {
            base.Update();

        }

        public void RotateTo(Quaternion quaternion)
        {
            if (quaternion == WeaponObject.transform.rotation) return;
            WeaponObject.transform.rotation = Quaternion.RotateTowards(WeaponObject.transform.rotation, quaternion,
                ConfigObject.Info.RotateSpeed * Time.deltaTime);
        }

        public void TryActivateAbility()
        {
            this.ActivateAbility();
        }

        public void DeactivateAbility()
        {
            Enable = false;
        }

        public void ActivateAbility()
        {
            Enable = true;
        }

        public void EndAbility()
        {
            EntityManager.Destroy(this);
        }

        /// <summary>
        /// ���ӵ�����ʱ
        /// </summary>
        /// <param name="targetEntity"></param>
        public void OnBulletTrigger(CombatEntity targetEntity)
        {
            AbilityEffectComponent.ApplyAllEffectsToTarget(targetEntity);
            Debug.Log(targetEntity.ShipData.ShipAttribute.curHullPoint.GetValueInt());
        }

        public void TryFireBullet()
        {
            //�����������ȴ��������״̬
            if(Enable && !IsCold)
            {
                WeaponExecution execution;
                if (ConfigObject.Info.BulletFlightType == BulletFlightType.�䵯��)
                {
                    execution = OwnerEntity.AddChild<WeaponExecution>(this);
                }
                else if (ConfigObject.Info.BulletFlightType == BulletFlightType.������)
                {
                    if (_execution == null || _execution.IsDisposed) { _execution = OwnerEntity.AddChild<WeaponExecution>(this); }
                    execution = _execution;
                }
                else
                {
                    execution = null;
                }
                execution.InputPoint = TargetPos;
                execution.InputDirection = (TargetPos - this.ParentEntity.Position).normalized;
                execution.Name = this.Name;
                execution.BeginExecute();
            }
        }

        private void SingleShot()
        {

        }
    }
}
