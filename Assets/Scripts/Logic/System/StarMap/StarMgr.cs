using Exterior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ϵ��ͼ����
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
    //��������λ��
    private Vector3 _spaceCenterPosition;
    public GameObject _starMapCamera;
    //��ϵ�����������
    public int maxGenerateCount = 200;
    //����������ϵ��
    public int _coreStarCount = 50;
    //�ڲ�������ϵ��
    public int _interStarCount = 100;
    //���������ϵ��
    public int _outerStarCount = 200;
    //��ǰ��������ϵ����
    public int generatedCount = 0;
    //��ϵ���ɷ�Χ
    private Range _zRange = new Range() { max = 10, min = -10 };
    private Range _radiusRange = new Range() { max = 500, min = 100 };
    //��ϵ��С����
    private float _starMinDistance = 25;
    //���ռ亽��֧·��Χ
    private float _channelMaxDistance = 45f;
    //���ռ亽��֧·���ɸ���
    private float _channelProbability = 0.2f;
    //��ϵ������Ϣ
    private List<GameObject> _starObjects = new List<GameObject>();
    //��ϵID����
    private List<string> _starId = new List<string>();
    //���ռ亽��Ԥ����
    private GameObject _channelPrefab;
    //��ϵ������������������ֹ��ѭ��
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
    /// ����������ϵ
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
    /// ���ؾ����ϵ������Զ��ϵ
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
    /// ���ɺ���
    /// </summary>
    /// <param name="connectStar"></param>
    /// <param name="unconnectStar"></param>
    private IEnumerator GenerateChannel(List<GameObject> connectStar, List<GameObject> unconnectStar)
    {
        GameObject cstar = null;
        GameObject ucstar = null;
        //ʹ��Prim�㷨������С��������·����
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
        //���ݲ�����������֧·����
        foreach (var starObject in _starObjects)
        {
            GenerateBranchChanel(starObject, _channelProbability);
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// ���ݲ����������ɺ���
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
                //���nstarû�����ӵ�Star����Starû�����ӵ�nstar
                if (!nstarInfo.connectStar.Contains(star)&&!starInfo.connectStar.Contains(nstar))
                {
                    //���ݸ������ɷ�֧����
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
    /// ��ȡ������ϵ
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
    /// ���ɳ��ռ亽��
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
        //��������������ʼID+"-"+����ID
        chanel.name = startStar.name + "-" + endStar.name;
        StarData[startStar.name].connectStar.Add(endStar);
        return chanel;
    }


    /// <summary>
    /// ��һ��Բ�η�Χ��������ϵ
    /// </summary>
    /// <param name="radiusMin"></param>
    /// <param name="radiusMax"></param>
    /// <param name="generateStarCount"></param>
    /// <param name="nextMethod">��һ��Ҫ����Я�̵ķ���</param>
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
            //���ɵ�һ����ϵ
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
                //�����ϵ���������С�����������ϵ
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
    /// ͨ��Prim�㷨���ӳ���·����Ȼ��ÿ���ڵ�������ӱ�
    /// </summary>
    private IEnumerator ConnectAllStar()
    {
        //��������ϵ
        List<GameObject> connectStar = new List<GameObject>();
        //δ���ӵ���ϵ
        List<GameObject> unconnectedStar = new List<GameObject>();
        //�����б�
        _starObjects.ForEach(t => unconnectedStar.Add(t));

        //���ɺ���
        var star = unconnectedStar[0];
        unconnectedStar.Remove(star);
        connectStar.Add(star);
        yield return StartCoroutine(GenerateChannel(connectStar, unconnectedStar));
    }

    /// <summary>
    /// Բ�ξ����������㷨
    /// </summary>
    private Vector3 CircularGenerate(float radius)
    {
        var z = Random.Range(_zRange.min, _zRange.max);
        // r��theta������Ҫ�ֱ������������r��thetaҪ�������
        var r = radius;
        var randomValue = Random.Range(0f, 1f);
        var theta = 2 * Mathf.PI * randomValue;
        //����x��y���꣬
        var x = r * Mathf.Cos(theta);
        var y = r * Mathf.Sin(theta);
        return new Vector3(x, z, y);
    }

    /// <summary>
    /// ����һ����ϵ
    /// </summary>
    /// <param name="starData"></param>
    /// <param name="starMaterials"></param>
    /// <param name="generatePos"></param>
    private void GenerateStar(Star starData,Vector3 generatePos)
    {
        var star = ResMgr.Instance.GetInstance(_starPrefabs[starData.starTypeIndex], _starRoot.transform);
        star.transform.position = generatePos;
        star.name = starData.id;

        //����������Ϣ
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


    //��ʾ��Χ
    private struct Range
    {
        public float max;
        public float min;
    }

}
