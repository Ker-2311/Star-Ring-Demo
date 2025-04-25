using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ECS.Combat
{
    /// <summary>
    /// ����ִ���壬���������ӵ��������Ӧ�ӵ��˶����
    /// </summary>
    public sealed class WeaponExecution : Entity, IExecution
    {
        public Entity AbilityEntity { get; set; }
        public CombatEntity OwnerEntity { get; set; }
        public WeaponAbility WeaponAbility { get { return AbilityEntity as WeaponAbility; } }
        /// <summary>
        /// �Ӵ����Ķ���
        /// </summary>
        public List<CombatEntity> WeaponTargets { get; set; } = new List<CombatEntity>();
        public CombatEntity InputTarget { get; set; }
        public Vector2 InputPoint { get; set; }
        public Vector2 InputDirection { get; set; }

        private GameObject _weaponExecutionObj;

        //����ִ��
        private bool _isExecuting = false;
        //��û�������������¿��Զ����е�ʱ��,��������ʹ��
        public float RunningTime { get; set; } = 0;


        public override void Awake(object initData)
        {
            AbilityEntity = initData as WeaponAbility;
        }

        public void BeginExecute()
        {
            if (WeaponAbility.ConfigObject.BulletPrefab == null) return;
            if (_isExecuting) { RunningTime = 0.1f;  return; }

            RunningTime = 0.1f;
            OwnerEntity = GetParent<CombatEntity>();
            _isExecuting = true;

            _weaponExecutionObj = GameObjectPool.Instance.GenerateObject(WeaponAbility.ConfigObject.Info.Name, WeaponAbility.
    WeaponObject.transform.position, WeaponAbility.WeaponObject.transform.rotation, WeaponAbility.ConfigObject.BulletPrefab, CombatContextEntity.Instance.BulletsObject);
            switch (WeaponAbility.ConfigObject.Info.BulletFlightType)
            {
                case BulletFlightType.�䵯��:
                    ProjectileFly(_weaponExecutionObj);break;
                case BulletFlightType.������:
                    LaserFly(_weaponExecutionObj); break;
            }
        }

        public void EndExecute()
        {
            WeaponTargets.Clear();
            GameObjectPool.Instance.CollectObject(_weaponExecutionObj);
            _isExecuting = false;
            EntityManager.Destroy(this);
        }

        /// <summary>
        /// �䵯��
        /// </summary>
        /// <param name="bullet"></param>
        private void ProjectileFly(GameObject bullet)
        {
            this.AddComponent<ProjectileFlyComponent>(bullet);
            WeaponAbility.ColdComponent.EnterCold(WeaponAbility.ConfigObject.Info.FireCd);
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="bullet"></param>
        private void LaserFly(GameObject bullet)
        {
            this.AddComponent<LaserFlyComponent>(bullet);
            _weaponExecutionObj.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

    }
}
