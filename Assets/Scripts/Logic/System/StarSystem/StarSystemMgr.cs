using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ϵ�ڲ�
/// </summary>
public class StarSystemMgr : Singleton<StarSystemMgr>
{
    public string CurStarID;//��ǰ��ϵID
    private GameObject _spaceRoot;
    private GameObject _starRoot;
    private GameObject[] _sunPrefabs;
    private GameObject _orbitPrefab;
    private GameObject _playerPrefab;
    private GameObject _starSystemRoot;
    private StarMapCameraControl _starCameraControll;
    private FightCameraControll _fightCameraControll;
    private GameObject[] _planetPrefabs;
    //����ϵ�ڲ���
    public bool OnSystem = false;
    //��������������
    public float PlanetRailRadius = 500;
    //���������뱶����ϵ
    private List<float> _planetRailRange = new List<float>() { 0.4f, 0.7f, 1f, 1.6f, 2.8f, 5.2f, 10f };
    //�ϴν������ϵID
    private string _lastStarId;
    //UI���
    private GameObject _fightPanel;
    private GameObject _mainPanel;

    public void Init(GameObject space)
    {
        _spaceRoot = space;
        _sunPrefabs = Resources.LoadAll<GameObject>("Prefabs/StarMapSystem/Sun");
        _orbitPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/StarMapSystem/Orbit");
        _playerPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Player/������");
        _starSystemRoot = _spaceRoot.transform.Find("StarSystem").gameObject;
        _starCameraControll = _spaceRoot.transform.Find("StarMapCamera").GetComponent<StarMapCameraControl>();
        _starRoot = _spaceRoot.transform.Find("Star").gameObject;
        _planetPrefabs = Resources.LoadAll<GameObject>("Prefabs/StarMapSystem/Planets");
        _fightCameraControll = _starSystemRoot.transform.Find("FightCamera").GetComponent<FightCameraControll>();
        _fightPanel = UIManager.Instance.GetUI("FightPanel", UIManager.UILayer.FightUI);
        _mainPanel = UIManager.Instance.GetUI("MainPanel", UIManager.UILayer.Top);
    }

    /// <summary>
    /// ������ϵ�ڲ�
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

        //����ϴν������ϵ����ͬһ����ϵ���������
        if (_lastStarId != starID)
        {
            var planets = _starSystemRoot.transform.Find("Planets");
            var presentDays = GameTimeMgr.Instance.GetDays();
            var fightPanel = _starSystemRoot.transform.Find("FightScene");

            //�������
            var player = ResMgr.Instance.GetInstance(_playerPrefab, fightPanel);
            var angle = Random.Range(0, 2 * Mathf.PI);
            player.transform.position = (500 + Random.Range(-200f, 200f)) *
                new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            //���ɺ���
            var sunRoot = _starSystemRoot.transform.Find("Sun");
            sunRoot.gameObject.DestroyChilds();
            var sun = ResMgr.Instance.GetInstance(_sunPrefabs[star.starTypeIndex], sunRoot);
            sun.transform.localPosition = Vector3.zero;

            planets.gameObject.DestroyChilds();
            //ѭ����������
            foreach (var planet in star.planets)
            {
                //��360��ȡģ�����ж�����ʱ��仯�ļ���
                planet.Angle += (presentDays - planet.LastEnterDays) % 360;
                if (planet.Angle >= 360)
                {
                    planet.Angle -= 360;
                }
                //��������
                var planetObject = ResMgr.Instance.GetInstance(_planetPrefabs[planet.PlanetTypeIndex], planets);
                planetObject.transform.localPosition = PlanetRailRadius * _planetRailRange[planet.RailIndex]
                    * new Vector3(Util.RadiansCos(planet.Angle), Util.RadiansSin(planet.Angle),0);
                //��ʾ������
                var orbitObject = ResMgr.Instance.GetInstance(_orbitPrefab, planetObject.transform, false);
                var orbit = orbitObject.GetComponent<PlanetOrbit>();
                orbit.Radius = PlanetRailRadius * _planetRailRange[planet.RailIndex];
                orbit.GenerateOrbit(_starSystemRoot.transform);
            }

            _lastStarId = starID;
        }

    }

    /// <summary>
    /// �˳���ϵ�ڲ�
    /// </summary>
    public void ExitStarSystem()
    {
        OnSystem = false;
        //��ʾ�����沢ɾ��ս��UI
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
