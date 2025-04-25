using ECS.Combat;
using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour,IBaseShip
{
    public string ID { get; set; }
    public CombatEntity CombatEntity { get; set; }

    //状态条
    public GameObject StatusSlider { get; set; }

    public NPCShip NPCShip { get; set; }

    public void Setup(NPCShip ship)
    {
        NPCShip = ship;

        //ECS初始化
        CombatEntity = CombatContextEntity.Instance.AddChild<CombatEntity>();
        CombatEntity.EntityTransform = transform;
        CombatEntity.ShipData = ship;
        CombatEntity.Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //状态栏不空即舰船在屏幕范围内时更新状态栏信息
        if (StatusSlider != null)
        {
            var shipScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            //给状态栏一个偏移
            StatusSlider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(shipScreenPos.x, shipScreenPos.y + 50f,0);
        }
    }

    private void OnBecameVisible()
    {
        StatusSlider = CombatContextEntity.Instance.GetStatusSlider();
    }

    private void OnBecameInvisible()
    {
        CombatContextEntity.Instance.CollectStatusSlider(StatusSlider);
        StatusSlider = null;
    }
}

