using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理星系内部
/// </summary>
public class StarSystemMgr : Singleton<StarSystemMgr>
{
    public string CurStarID;//当前星系ID
    private GameObject _spaceRoot;
    private GameObject _starRoot;
    private GameObject[] _sunPrefabs;
    private GameObject _orbitPrefab;
    private GameObject _playerPrefab;
    private GameObject _starSystemRoot;
    private StarMapCameraControl _starCameraControll;
    private FightCameraControll _fightCameraControll;
    private GameObject[] _planetPrefabs;
    //在星系内部？
    public bool OnSystem = false;
    //星球轨道基本距离
    public float PlanetRailRadius = 500;
    //星球轨道距离倍数关系
    private List<float> _planetRailRange = new List<float>() { 0.4f, 0.7f, 1f, 1.6f, 2.8f, 5.2f, 10f };
    //上次进入的星系ID
    private string _lastStarId;
    //UI相关
    private GameObject _fightPanel;
    private GameObject _mainPanel;

    public void Init(GameObject space)
    {
        _spaceRoot = space;
        _sunPrefabs = Resources.LoadAll<GameObject>("Prefabs/StarMapSystem/Sun");
        _orbitPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/StarMapSystem/Orbit");
        _playerPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Player/护卫舰");
        _starSystemRoot = _spaceRoot.transform.Find("StarSystem").gameObject;
        _starCameraControll = _spaceRoot.transform.Find("StarMapCamera").GetComponent<StarMapCameraControl>();
        _starRoot = _spaceRoot.transform.Find("Star").gameObject;
        _planetPrefabs = Resources.LoadAll<GameObject>("Prefabs/StarMapSystem/Planets");
        _fightCameraControll = _starSystemRoot.transform.Find("FightCamera").GetComponent<FightCameraControll>();
        _fightPanel = UIManager.Instance.GetUI("FightPanel", UIManager.UILayer.FightUI);
        _mainPanel = UIManager.Instance.GetUI("MainPanel", UIManager.UILayer.Top);
    }

    /// <summary>
    /// 进入星系内部
    /// </summary>
    /// <param name="starID"></param>
    public void EnterStarSystem(string starID)
    {
        //var viewPoint = _starSystemRoot.transform.Find("ViewPoint");
        var star = StarMgr.Instance.GetStarData()[starID];
        CurStarID = starID;
        OnSystem = true;
        _starRoot.SetActive(false);
        _starSystemRoot.SetActive(true);

        _fightCameraControll.gameObject.SetActive(true);
        _starCameraControll.gameObject.SetActive(false);

        //如果上次进入的星系不是同一个星系则进行生成
        if (_lastStarId != starID)
        {
            var planets = _starSystemRoot.transform.Find("Planets");
            var presentDays = GameTimeMgr.Instance.GetDays();
            var fightPanel = _starSystemRoot.transform.Find("FightScene");

            //生成玩家
            var player = ResMgr.Instance.GetInstance(_playerPrefab, fightPanel);
            var angle = Random.Range(0, 2 * Mathf.PI);
            player.transform.position = (500 + Random.Range(-200f, 200f)) *
                new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            //生成恒星
            var sunRoot = _starSystemRoot.transform.Find("Sun");
            sunRoot.gameObject.DestroyChilds();
            var sun = ResMgr.Instance.GetInstance(_sunPrefabs[star.starTypeIndex], sunRoot);
            sun.transform.localPosition = Vector3.zero;

            planets.gameObject.DestroyChilds();
            //循环生成星球
            foreach (var planet in star.planets)
            {
                //对360度取模，进行度数随时间变化的计算
                planet.Angle += (presentDays - planet.LastEnterDays) % 360;
                if (planet.Angle >= 360)
                {
                    planet.Angle -= 360;
                }
                //生成星球
                var planetObject = ResMgr.Instance.GetInstance(_planetPrefabs[planet.PlanetTypeIndex], planets);
                planetObject.transform.localPosition = PlanetRailRadius * _planetRailRange[planet.RailIndex]
                    * new Vector3(Util.RadiansCos(planet.Angle), Util.RadiansSin(planet.Angle),0);
                //显示星球轨道
                var orbitObject = ResMgr.Instance.GetInstance(_orbitPrefab, planetObject.transform, false);
                var orbit = orbitObject.GetComponent<PlanetOrbit>();
                orbit.Radius = PlanetRailRadius * _planetRailRange[planet.RailIndex];
                orbit.GenerateOrbit(_starSystemRoot.transform);
            }

            _lastStarId = starID;
        }

    }

    /// <summary>
    /// 退出星系内部
    /// </summary>
    public void ExitStarSystem()
    {
        OnSystem = false;
        //显示主界面并删除战斗UI
        UIManager.Instance.ActivateLayer(UIManager.UILayer.Top);
        FightUIMgr.Instance.RemoveFightUI();
        _starRoot.SetActive(true);
        _starSystemRoot.SetActive(false);
        _starCameraControll.gameObject.SetActive(true);
        _fightCameraControll.gameObject.SetActive(false);
    }

    public void ShowFightUI()
    {
        _mainPanel.SetActive(false);
        _fightPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        _mainPanel.SetActive(true);
        _fightPanel.SetActive(false);
    }
}
