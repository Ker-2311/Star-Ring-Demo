using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    /// <summary>
    /// ����������룬����������ΧԤ��
    /// </summary>
    public class InputComponent : ECSComponent
    {
        private CombatEntity _combatEntity;

        //���ڳ�������
        private bool _startFire = false;
        //���λ��
        private Vector3 _inputPoint = Vector3.zero;

        public override void Awake()
        {
            base.Awake();
            _combatEntity = Entity as CombatEntity;
        }

        public override void Update()
        {
            base.Update();
            var moveComponent = _combatEntity.GetComponent<MoveComponent>();
            var fightCamera = CombatContextEntity.Instance.FightCamera;

            CalculateInputPoint();
            _combatEntity.ActiveMainCannon.TargetPos = _inputPoint;
            MainCannonRotate();
            StartFire(_combatEntity.ActiveMainCannon);

            //������
            if (Input.GetMouseButtonDown((int)UnityEngine.UIElements.MouseButton.LeftMouse))
            {
                _startFire = true;
            }
            if (Input.GetMouseButtonUp((int)UnityEngine.UIElements.MouseButton.LeftMouse))
            {
                _startFire = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeActiveWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeActiveWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeActiveWeapon(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeActiveWeapon(4);
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveComponent.Accelerate();
                moveComponent.ClearPoint();
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveComponent.Decelerate();
                moveComponent.ClearPoint();
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveComponent.LeftTurn();
                moveComponent.ClearPoint();
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveComponent.RightTurn();
                moveComponent.ClearPoint();
            }
            //û���κ��ƶ�
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                moveComponent.Idle();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                fightCamera.GetComponent<FightCameraControll>().MouseOffset(Input.mousePosition);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                fightCamera.GetComponent<FightCameraControll>().EndMouseOffset();
            }
        }

        private void StartFire(WeaponAbility ability)
        {
            if (!_startFire) { return; }
            if (ability == null) { return; }

            ability.TryFireBullet();
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void CalculateInputPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //����Ͷ�䵽ս��
            if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Context")))
            {
                _inputPoint = hit.point;
            }
        }

        /// <summary>
        /// ������תָ�����
        /// </summary>
        public void MainCannonRotate()
        {
            foreach(var cannon in _combatEntity.EquipedMainCannon)
            {
                if (cannon != null)
                {
                    var dir = (Vector2)(_inputPoint - cannon.ParentEntity.Position).normalized;

                    cannon.RotateTo(Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, dir)));
                }
            }
        }

        /// <summary>
        /// �л����������
        /// </summary>
        /// <param name="index"></param>
        public void ChangeActiveWeapon(int index)
        {
            if (index <= _combatEntity.EquipedMainCannon.Length && _combatEntity.EquipedMainCannon[index - 1] != null)
            {
                _combatEntity.ActiveMainCannon?.DeactivateAbility();
                _combatEntity.ActiveMainCannon = _combatEntity.EquipedMainCannon[index - 1];
                _combatEntity.ActiveMainCannon?.ActivateAbility();
            }
        }
    }
}

