using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class ProjectileFlyComponent : ECSComponent
    {
        /// <summary>
        /// 子弹对象
        /// </summary>
        private GameObject _bullet;
        private IExecution _execution;
        //飞行距离
        private float _flyDistance = 0;
        public override void Awake(object initData)
        {
            base.Awake(initData);
            _bullet = initData as GameObject;
            _execution = this.Entity as IExecution;
        }

        public override void Update()
        {
            base.Update();
            if (_execution is WeaponExecution)
            {
                var weaponExecution = _execution as WeaponExecution;
                var distance = _bullet.transform.right * weaponExecution.WeaponAbility.ConfigObject.Info.BulletSpeed * Time.deltaTime;
                //当子弹飞行距离超出武器射程时结束执行
                if (_flyDistance >= weaponExecution.WeaponAbility.ConfigObject.Info.Range)
                {
                    weaponExecution.EndExecute();
                }
                _bullet.transform.position += distance;
                _flyDistance += distance.magnitude;
            }
        }
    }
}

