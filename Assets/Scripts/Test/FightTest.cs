using ECS;
using ECS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTest : MonoBehaviour
{
    private void Awake()
    {
        GameMgr.Instance.SetupInit();
        StartCoroutine(GameMgr.Instance.LoadingInit());
        FightUIMgr.Instance.AddFightUI();
        CombatContextEntity.Instance = EntityManager.Create<CombatContextEntity>();
        PlayerShipMgr.Instance.InstallWeapon(EquipmentMgr.Instance.GetEquipment<Weapon>("938583"),"1");
        PlayerShipMgr.Instance.InstallWeapon(EquipmentMgr.Instance.GetEquipment<Weapon>("926921"), "0");
        //生成玩家
        var _playerPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Player/护卫舰");
        var _npcPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/NPC/帝国/维度行者");
        var _starSystemRoot = GameObject.Find("StarSystem");
        var planets = _starSystemRoot.transform.Find("Planets");
        var presentDays = GameTimeMgr.Instance.GetDays();
        var fightPanel = _starSystemRoot.transform.Find("FightScene");
        CombatContextEntity.Instance.AddPlayer(_playerPrefab,fightPanel, new Vector3(500, 500, 0));
        CombatContextEntity.Instance.AddNPC(_npcPrefab, fightPanel, new Vector3(500, 510, 0));
        var angle = Random.Range(0, 2 * Mathf.PI);
    }
}
