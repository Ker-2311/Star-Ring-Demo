using ECS.Combat;
using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour,IBaseShip
{
    public string ID { get; set; }
    public CombatEntity CombatEntity { get; set; }

    //״̬��
    public GameObject StatusSlider { get; set; }

    public NPCShip NPCShip { get; set; }

    public void Setup(NPCShip ship)
    {
        NPCShip = ship;

        //ECS��ʼ��
        CombatEntity = CombatContextEntity.Instance.AddChild<CombatEntity>();
        CombatEntity.EntityTransform = transform;
        CombatEntity.ShipData = ship;
        CombatEntity.Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //״̬�����ռ���������Ļ��Χ��ʱ����״̬����Ϣ
        if (StatusSlider != null)
        {
            var shipScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            //��״̬��һ��ƫ��
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

