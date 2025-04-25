using ECS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;
using System;

public class Player : MonoBehaviour,IBaseShip
{
    public CombatEntity CombatEntity { get; set; }

    /// <summary>
    /// 玩家舰船数据，如果是NPC则为空
    /// </summary>
    public PlayerShip PlayerShip { get; set; }
    public string ID { get; set; }

    //武器根对象
    private GameObject _weaponRoot;

    public void Setup()
    {
        Camera _fightCamera;
        FightCameraControll _fightCameraControll;
        _fightCamera = CombatContextEntity.Instance.FightCamera;
        _fightCameraControll = _fightCamera.GetComponent<FightCameraControll>();
        _weaponRoot = transform.Find("Weapon").gameObject;
        _fightCameraControll.SetTarget(transform, new Vector3(0, 0, -20), new Vector3(0, 0, 0));

        PlayerShip = PlayerShipMgr.Instance.GetActivateShip();

        //ECS初始化
        CombatEntity = CombatContextEntity.Instance.AddChild<CombatEntity>();
        CombatEntity.ShipData = PlayerShip;
        CombatEntity.EntityTransform = transform;
        CombatEntity.Rigidbody = GetComponent<Rigidbody2D>();
        CombatEntity.AddComponent<InputComponent>();
        CombatEntity.AnimationComponent = GetComponent<ShipAnimationComponent>();
        //CombatEntity.Rigidbody.isKinematic = true;

        //将已装备武器Ability挂载
        foreach (var slot in _weaponRoot.GetAllChilds())
        {
            var weapon = PlayerShipMgr.Instance.GetWeapon(slot.name);
            if (weapon != null)
            {
                var configObject = ConfigOperation.GetWeaponConfigObject(weapon.WeaponInfo.Name);
                var weaponObject = ResMgr.Instance.GetInstance(configObject.WeaponPrefab, slot.transform);
                CombatEntity.AttachMainCannonAbility(configObject, weaponObject, Convert.ToInt32(slot.name));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    //射线投射到战场
        //    if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Context")))
        //    {
        //        var pos = new Vector2(hit.point.x, hit.point.y);
        //        CombatEntity.GetComponent<MoveComponent>().SetTargetPoint(pos);
        //    }
        //}

    }
}
