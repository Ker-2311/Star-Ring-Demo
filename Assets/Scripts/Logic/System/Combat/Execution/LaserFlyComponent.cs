using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ECS.Combat
{
    public class LaserFlyComponent : ECSComponent
    {
        /// <summary>
        /// 子弹对象
        /// </summary>
        private GameObject _bullet;
        private IExecution _execution;
        private LineRenderer _lineRenderer;

        private Transform _endPoint;
        //效果触发冷却时间
        private float _effectTriggerColdTime = 0f; 
        public override void Awake(object initData)
        {
            base.Awake(initData);
            _bullet = initData as GameObject;
            _execution = this.Entity as IExecution;
            _lineRenderer = _bullet.GetComponent<LineRenderer>();
            _endPoint = _bullet.transform.Find("EndPoint");
        }

        public override void Update()
        {
            base.Update();
            if (_execution is WeaponExecution)
            {
                var weaponExecution = _execution as WeaponExecution;
                var weaponPos = (Vector2)weaponExecution.WeaponAbility.WeaponObject.transform.position;
                var weaponInfo = weaponExecution.WeaponAbility.ConfigObject.Info;
                var weaponDir = (Vector2)weaponExecution.WeaponAbility.WeaponObject.transform.right.normalized;
                var endPos =  weaponDir * weaponInfo.Range;

                if (weaponExecution.RunningTime <= 0)
                {
                    weaponExecution.EndExecute();
                }

                //射线检测两次，第一次是自身舰船，第二次则是目标舰船
                RaycastHit2D[] hit = new RaycastHit2D[2];
                ContactFilter2D contactFilter = new ContactFilter2D();
                contactFilter.layerMask = 1 << LayerMask.NameToLayer("Ship");

                Physics2D.Raycast(weaponPos, weaponDir, contactFilter,hit, weaponInfo.Range);

                _bullet.transform.position = weaponPos;
                if (hit[1] && hit[1].transform != weaponExecution.OwnerEntity.EntityTransform)
                {
                    var hitEntity = hit[1].transform.GetComponent<IBaseShip>();
                    endPos = hit[1].point - weaponPos;
                    if (_effectTriggerColdTime <= 0)
                    {
                        weaponExecution.WeaponAbility.OnBulletTrigger(hitEntity.CombatEntity);
                        _effectTriggerColdTime = weaponInfo.FireCd;
                    }
                }
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, endPos);
                _endPoint.transform.localPosition = endPos;
                weaponExecution.RunningTime -= Time.deltaTime;
                _effectTriggerColdTime -= Time.deltaTime;

            }
        }
    }
}
