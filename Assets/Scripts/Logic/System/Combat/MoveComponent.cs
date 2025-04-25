using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class MoveComponent : ECSComponent
    {
        public CombatEntity CombatEntity { get; set; }

        public float MaxSpeed {
            get 
            {
                return CombatEntity.ShipData.ShipAttribute.maxShipSpeed.GetValueFloat(); 
            }
            set
            {
                CombatEntity.ShipData.ShipAttribute.maxShipSpeed.SetValue(value);
            }
        }

        public float CurSpeed
        {
            get
            {
                return CombatEntity.ShipData.ShipAttribute.curShipSpeed.GetValueFloat();
            }
            set
            {
                CombatEntity.ShipData.ShipAttribute.curShipSpeed.SetValue(value);
            }
        }

        public float ShipAcceleration
        {
            get
            {
                return CombatEntity.ShipData.ShipAttribute.shipAcceleration.GetValueFloat();
            }
            set
            {
                CombatEntity.ShipData.ShipAttribute.shipAcceleration.SetValue(value);
            }
        }

        public float RotateSpeed
        {
            get
            {
                return CombatEntity.ShipData.ShipAttribute.ShipRotateSpeed.GetValueFloat();
            }
            set
            {
                CombatEntity.ShipData.ShipAttribute.ShipRotateSpeed.SetValue(value);
            }
        }

        /// <summary>
        /// 实际的RigidBody组件速度
        /// </summary>
        public Vector2 Velocity
        {
            get { return CombatEntity.Rigidbody.velocity; }
            set { CombatEntity.Rigidbody.velocity = value; }
        }

        //要移动到的目标点,不能为0
        public Vector2 TargetPoint { get; set; } = Vector2.zero;

        public override void Awake()
        {
            base.Awake();
            CombatEntity = Entity as CombatEntity;
        }

        public override void Update()
        {
            base.Update();

            var forwardDir = (Vector2)CombatEntity.EntityTransform.right;
            Velocity = forwardDir * CurSpeed * 0.1f;
            CombatEntity.Rigidbody.angularVelocity = 0f;

            MoveToTargetPoint();
        }

        /// <summary>
        /// 舰船加速
        /// </summary>
        public void Accelerate()
        {
            if (!Enable) return;
            AdjustVelocity(ShipAcceleration);
            CombatEntity.AnimationComponent.TryPlayFade(CombatEntity.AnimationComponent.AccelerateAnimation);
        }

        /// <summary>
        /// 舰船减速
        /// </summary>
        public void Decelerate()
        {
            if (!Enable) return;
            AdjustVelocity(-ShipAcceleration);
        }

        /// <summary>
        /// 左转
        /// </summary>
        public void LeftTurn()
        {
            if (!Enable) return;
            var angle = CombatEntity.EntityTransform.eulerAngles;
            angle.z += Time.deltaTime * RotateSpeed;
            CombatEntity.EntityTransform.rotation = Quaternion.Euler(angle);
            CombatEntity.AnimationComponent.TryPlayFade(CombatEntity.AnimationComponent.LeftTurnAnimation);
        }

        /// <summary>
        /// 右转
        /// </summary>
        public void RightTurn()
        {
            if (!Enable) return;
            var angle = CombatEntity.EntityTransform.eulerAngles;
            angle.z -= Time.deltaTime * RotateSpeed;
            CombatEntity.EntityTransform.rotation = Quaternion.Euler(angle);
            CombatEntity.AnimationComponent.TryPlayFade(CombatEntity.AnimationComponent.RightTurnAnimation);
        }

        public void Idle()
        {
            CombatEntity.AnimationComponent.TryPlayFade(CombatEntity.AnimationComponent.IdleAnimation);
        }

        /// <summary>
        /// 设置目标点
        /// </summary>
        /// <param name="point"></param>
        public void SetTargetPoint(Vector2 point)
        {
            TargetPoint = point;
        }

        public void ClearPoint()
        {
            TargetPoint = Vector2.zero;
        }

        /// <summary>
        /// 移动到目标点
        /// </summary>
        private void MoveToTargetPoint()
        {
            //移动至目标点
            if (TargetPoint != Vector2.zero)
            {
                var pos = new Vector2(CombatEntity.EntityTransform.position.x, CombatEntity.EntityTransform.position.y);
                var forward = new Vector2(CombatEntity.EntityTransform.right.x, CombatEntity.EntityTransform.right.y);
                var dir = TargetPoint - pos;
                var speedVec = new Vector2((CombatEntity.EntityTransform.right * Time.deltaTime).x,
                    (CombatEntity.EntityTransform.right * Time.deltaTime).y);
                var angle = Vector2.SignedAngle(forward, dir);
                if (angle > 1f)
                {
                    LeftTurn();
                }
                else if (angle < -1f)
                {
                    RightTurn();
                }
                if (dir.magnitude > (dir - speedVec).magnitude)
                {
                    Accelerate();
                }
                else if (dir.magnitude > (dir + speedVec).magnitude)
                {
                    Decelerate();
                }
                if (dir.magnitude < 1f)
                {
                    ClearPoint();
                }
            }
        }

        /// <summary>
        /// 调整实际速度
        /// </summary>
        /// <param name="value"></param>
        private void AdjustVelocity(float value)
        {
            float adjustValue = CurSpeed + value;
            //调整后超出最大速度
            if (adjustValue > MaxSpeed) adjustValue = MaxSpeed;
            //调整后小于最低速度
            else if (adjustValue < 0) adjustValue = 0;

            CurSpeed =  adjustValue;
        }
    }
}

