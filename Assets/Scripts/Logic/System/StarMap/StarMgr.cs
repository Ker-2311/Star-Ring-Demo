using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 星系地图管理
/// </summary>
public class StarMgr : MonoSingleton<StarMgr>
{
    private Dictionary<string, Star> StarData
    {
        get { return DataMgr.Instance.PlayerData.StarData; }
        set { DataMgr.Instance.PlayerData.StarData = value; }
    }

    private GameObject _spaceRoot;
    private GameObject _starRoot;
    private GameObject[] _starPrefabs;

    public bool generateSpaceFinished = false;
    //宇宙中心位置
    private Vector3 _spaceCenterPosition;
    public GameObject _starMapCamera;
    //星系最大生成数量
    public int maxGenerateCount = 200;
    //核心星域星系数
    public int _coreStarCount = 50;
    //内层星域星系数
    public int _interStarCount = 100;
    //外层星域星系数
    public int _outerStarCount = 200;
    //当前已生成星系数量
    public int generatedCount = 0;
    //星系生成范围
    private Range _zRange = new Range() { max = 10, min = -10 };
    private Range _radiusRange = new Range() { max = 500, min = 100 };
    //星系最小距离
    private float _starMinDistance = 25;
    //超空间航道支路范围
    private float _channelMaxDistance = 45f;
    //超空间航道支路生成概率
    private float _channelProbability = 0.2f;
    //星系对象信息
    private List<GameObject> _starObjects = new List<GameObject>();
    //星系ID缓存
    private List<string> _starId = new List<string>();
    //超空间航道预制体
    private GameObject _channelPrefab;
    //星系生成最大遍历数量，防止死循环
    private int _maxTraverseCount = 10000;

    public void Init(int seed,GameObject space)
    {

        Random.InitState(seed);
        _spaceRoot = space;
        _spaceCenterPosition = _spaceRoot.transform.position;
        _starMapCamera = _spaceRoot.transform.Find("StarMapCamera").gameObject;
        _starRoot = _spaceRoot.transform.Find("Star").gameObject;
        _starPrefabs = Resources.LoadAll<GameObject>("Prefabs/StarMapSystem/Star");
        _channelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/StarMapSystem/Line");
    }

    /// <summary>
    /// 生成所有星系
    /// </summary>
    /// <param name="starData"></param>
    public IEnumerator GenerateAllStar()
    {
        yield return StartCoroutine(GenerateCircleStarArea(100, 400, 200));
        //yield return StartCoroutine(GenerateCircleStarArea(600, 700, _interStarCount));
        //yield return StartCoroutine(GenerateCircleStarArea(900, 1000, _outerStarCount));
        yield return StartCoroutine(ConnectAllStar());
        generateSpaceFinished = true;
    }

    /// <summary>
    /// 返回距离河系中心最远星系
    /// </summary>
    /// <returns></returns>
    public GameObject GetTheFarthest()
    {
        float maxDistance = 0;
        GameObject farthestStar = null;
        foreach(var starObject in _starObjects)
        {
            var distance = (starObject.transform.position - _spaceRoot.transform.position).magnitude;
            if (distance > maxDistance)
            {
                farthestStar = starObject;
                maxDistance = distance;
            }
        }

        return farthestStar;
    }

    public Dictionary<string, Star> GetStarData()
    {
        Dictionary<string, Star> starData = new Dictionary<string, Star>();
        foreach (var data in StarData)
        {
            starData.Add(data.Key, data.Value);
        }
        return starData;
    }

    /// <summary>
    /// 生成航道
    /// </summary>
    /// <param name="connectStar"></param>
    /// <param name="unconnectStar"></param>
    private IEnumerator GenerateChannel(List<GameObject> connectStar, List<GameObject> unconnectStar)
    {
        GameObject cstar = null;
        GameObject ucstar = null;
        //使用Prim算法生成最小生成树主路航道
        while (connectStar.Count < maxGenerateCount)
        {
            float minDistance = float.PositiveInfinity;
            foreach (var unconStar in unconnectStar)
            {
                foreach (var conStar in connectStar)
                {
                    var distance = (unconStar.transform.position - conStar.transform.position).magnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        cstar = conStar;
                        ucstar = unconStar;
                    }
                }
            }
            if (cstar != null && ucstar != null)
            {
                GenerateChannel(cstar, ucstar);
                connectStar.Add(ucstar);
                unconnectStar.Remove(ucstar);
            }
            yield return new WaitForSeconds(2 * Time.deltaTime);

        }
        //根据参数概率生成支路航道
        foreach (var starObject in _starObjects)
        {
            GenerateBranchChanel(starObject, _channelProbability);
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 根据参数概率生成航道
    /// </summary>
    /// <param name="star"></param>
    private void GenerateBranchChanel(GameObject star,float index)
    {
        List<GameObject> near = GetNearStar(star,_channelMaxDistance);
        var starInfo = StarData[star.name];

        if (near.Count != 0)
        {
            foreach (var nstar in near)
            {
                var nstarInfo = StarData[nstar.name];
                //如果nstar没有连接到Star并且Star没有连接到nstar
                if (!nstarInfo.connectStar.Contains(star)&&!starInfo.connectStar.Contains(nstar))
                {
                    //根据概率生成分支航道
                    float i = Random.Range(0f, 1f);
                    if (i < index)
                    {
                        GenerateChannel(star, nstar);
                        continue;
                    }
                }
            }
        }
    }


    /// <summary>
    /// 获取近邻星系
    /// </summary>
    /// <param name="star"></param>
    /// <returns></returns>
    private List<GameObject> GetNearStar(GameObject star,float maxDistance)
    {
        List<GameObject> near = new List<GameObject>();
        foreach (var nearStar in _starObjects)
        {
            float distance = new Vector2(nearStar.transform.position.x - star.transform.position.x,
                nearStar.transform.position.z - star.transform.position.z).magnitude;
            if (nearStar.name != star.name && distance <= maxDistance)
            {
                near.Add(nearStar);
            }
        }
        return near;
    }

    /// <summary>
    /// 生成超空间航道
    /// </summary>
    /// <param name="startStar"></param>
    /// <param name="endStar"></param>
    /// <returns></returns>
    private GameObject GenerateChannel(GameObject startStar,GameObject endStar)
    {
        var chanel = ResMgr.Instance.GetInstance(_channelPrefab, startStar.transform);
        var line = chanel.GetComponent<LineRenderer>();
        line.SetPosition(0, startStar.transform.position);
        line.SetPosition(1, endStar.transform.position);
        //chanel.SetActive(false);
        //航道命名规则，起始ID+"-"+结束ID
        chanel.name = startStar.name + "-" + endStar.name;
        StarData[startStar.name].connectStar.Add(endStar);
        return chanel;
    }


    /// <summary>
    /// 在一个圆形范围内生成星系
    /// </summary>
    /// <param name="radiusMin"></param>
    /// <param name="radiusMax"></param>
    /// <param name="generateStarCount"></param>
    /// <param name="nextMethod">下一个要进行携程的方法</param>
    /// <returns></returns>
    private IEnumerator GenerateCircleStarArea(float radiusMin,float radiusMax,int generateStarCount)
    {
        int traverseCount = 0;
        int curCount = 0;
        while (curCount < generateStarCount && traverseCount <= _maxTraverseCount)
        {
            var radius = Random.Range(radiusMin, radiusMax);
            var generatePos = _spaceCenterPosition + CircularGenerate(radius);
            var id = IDFactory.GenerateIdFormTime();
            var starInfo = new Star() { id = id };
            float minStarDistance = 99999;
            //生成第一个星系
            _starId.Add(id);
            if (_starObjects.Count == 0)
            {
                GenerateStar(starInfo, generatePos);
                StarData.Add(id, starInfo);
                continue;
            }
            for (int i = 0; i < _starObjects.Count; i++)
            {
                var starObject = _starObjects[i];
                Vector2 distance = new Vector2(generatePos.x - starObject.transform.position.x,
                    generatePos.z - starObject.transform.position.z);
                //如果星系间隔大于最小间隔则生成星系
                if (distance.magnitude <= minStarDistance)
                {
                    minStarDistance = distance.magnitude;
                }
            }
            if (minStarDistance > _starMinDistance)
            {
                GenerateStar(starInfo, generatePos);
                StarData.Add(id, starInfo);
                curCount++;
                generatedCount++;
            }
            traverseCount++;
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 通过Prim算法连接出主路径，然后每个节点随机增加边
    /// </summary>
    private IEnumerator ConnectAllStar()
    {
        //已连接星系
        List<GameObject> connectStar = new List<GameObject>();
        //未连接的星系
        List<GameObject> unconnectedStar = new List<GameObject>();
        //复制列表
        _starObjects.ForEach(t => unconnectedStar.Add(t));

        //生成航道
        var star = unconnectedStar[0];
        unconnectedStar.Remove(star);
        connectStar.Add(star);
        yield return StartCoroutine(GenerateChannel(connectStar, unconnectedStar));
    }

    /// <summary>
    /// 圆形均布点生成算法
    /// </summary>
    private Vector3 CircularGenerate(float radius)
    {
        var z = Random.Range(_zRange.min, _zRange.max);
        // r和theta的生成要分别生成随机数，r和theta要互不相干
        var r = radius;
        var randomValue = Random.Range(0f, 1f);
        var theta = 2 * Mathf.PI * randomValue;
        //生成x，y坐标，
        var x = r * Mathf.Cos(theta);
        var y = r * Mathf.Sin(theta);
        return new Vector3(x, z, y);
    }

    /// <summary>
    /// 生成一个星系
    /// </summary>
    /// <param name="starData"></param>
    /// <param name="starMaterials"></param>
    /// <param name="generatePos"></param>
    private void GenerateStar(Star starData,Vector3 generatePos)
    {
        var star = ResMgr.Instance.GetInstance(_starPrefabs[starData.starTypeIndex], _starRoot.transform);
        star.transform.position = generatePos;
        star.name = starData.id;

        //生成星球信息
        var planetCount = Random.Range(3, 8);
        var planets = new List<Planet>();
        for (int i = 0;i< planetCount;i++)
        {
            var planet = new Planet();
            planet.Angle = Random.Range(0,360);
            planet.RailIndex = i;
            planets.Add(planet);
        }
        starData.planets = planets;

        _starObjects.Add(star);
    }


    //表示范围
    private struct Range
    {
        public float max;
        public float min;
    }

}
