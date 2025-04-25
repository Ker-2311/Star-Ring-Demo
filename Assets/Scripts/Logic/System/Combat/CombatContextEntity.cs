using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public class CombatContextEntity : SingletonEntity<CombatContextEntity>
    {
        public GameObject StarSystemRoot { get; set; }

        public GameObject FightSceneRoot { get; set; }
        public Camera FightCamera { get; set; }
        public Transform BulletsObject { get; set; }

        public CombatEntity Player { get; set; }
        public Dictionary<string, CombatEntity> NPCDic{get;set;}

        //NPC状态栏预制体
        private GameObject _statusPrefab;
        //伤害信息预制体
        private GameObject _damageinfoPrefab;

        //计数器
        private int _count = 0;

        private GameObject _fightPanel;

        public override void Awake()
        {
            base.Awake();
            StarSystemRoot = GameObject.Find("StarSystem");
            FightSceneRoot = StarSystemRoot.transform.Find("FightScene").gameObject;
            BulletsObject = FightSceneRoot.transform.Find("Bullets");
            FightCamera = StarSystemRoot.transform.Find("FightCamera").GetComponent<Camera>();
            _statusPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/FightUI/NPCStatus");
            NPCDic = new Dictionary<string, CombatEntity>();
            _fightPanel = UIManager.Instance.GetUI("FightPanel", UIManager.UILayer.FightUI);
        }

        /// <summary>
        /// 往战场上添加玩家
        /// </summary>
        public GameObject AddPlayer(GameObject prefab,Transform fightScene)
        {
            var playerObject = ResMgr.Instance.GetInstance(prefab, fightScene, false);
            var player = playerObject.GetComponent<Player>();
            var id = GenerateID();
            playerObject.name = id;
            player.Setup();
            player.ID = id;
            Player = player.CombatEntity;
            return playerObject;
        }

        public GameObject AddPlayer(GameObject prefab, Transform fightScene,Vector3 pos)
        {
            var player = AddPlayer(prefab, fightScene);
            player.transform.position = pos;
            return player;
        }

        public GameObject AddNPC(GameObject prefab, Transform fightScene)
        {
            var NPCObject = ResMgr.Instance.GetInstance(prefab, fightScene, false);
            var npc = NPCObject.GetComponent<NPC>();
            var id = GenerateID();
            NPCObject.name = id;
            npc.Setup(NPCShipMgr.Instance.GetShip( prefab.name));
            npc.ID = id;
            NPCDic.Add(id, npc.CombatEntity);
            return NPCObject;
        }

        public GameObject AddNPC(GameObject prefab, Transform fightScene,Vector3 pos)
        {
            var NPC = AddNPC(prefab, fightScene);
            NPC.transform.position = pos;
            return NPC;
        }

        private string GenerateID()
        {
            _count++;
            return (10000 + _count).ToString();
        }


        public GameObject GetStatusSlider()
        {
            return GameObjectPool.Instance.GenerateObject(_statusPrefab.name, Vector3.zero, Quaternion.Euler(Vector3.zero), _statusPrefab,
                _fightPanel.transform.Find("NPCStatus"));
        }

        public void CollectStatusSlider(GameObject slider)
        {
            if (slider.name != _statusPrefab.name) return;
            GameObjectPool.Instance.CollectObject(slider);
        }

        public GameObject GetDamageInfo()
        {
            return GameObjectPool.Instance.GenerateObject(_damageinfoPrefab.name, Vector3.zero, Quaternion.Euler(Vector3.zero),
                _damageinfoPrefab,_fightPanel.transform.Find("DamageInfos"));
        }

        public void CollectDamageInfo(GameObject info)
        {
            if (info.name != _damageinfoPrefab.name) return;
            GameObjectPool.Instance.CollectObject(info);
        }
    }
}

