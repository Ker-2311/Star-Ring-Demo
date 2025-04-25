using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池基类
/// </summary>
public class GameObjectPool:Singleton<GameObjectPool>
{
    private Dictionary<string, List<GameObject>> _cache = new Dictionary<string, List<GameObject>>();

    /// <summary>
    /// 通过池生成一个对象，如果池中没有则创建
    /// </summary>
    /// <param name="name">对象池名字</param>
    /// <param name="Pos"></param>
    /// <param name="Rot"></param>
    /// <param name="parent"></param>
    public GameObject GenerateObject(string name,Vector3 Pos,Quaternion Rot,GameObject prefab,Transform parent = null)
    {
        GameObject obj = null;
        //查找在池中的disable对象并幅值给obj
        if (_cache.ContainsKey(name))
        {
            foreach (var gameobject in _cache[name])
            {
                if (!gameobject.activeInHierarchy)
                {
                    obj = gameobject;
                    break;
                }
            }
        }
        //如果池中无可激活对象则生成一个
        if (obj == null)
        {
            obj = ResMgr.Instance.GetInstance(prefab, parent);
            if (_cache.ContainsKey(name))
            {
                _cache[name].Add(obj);
            }
            else
            {
                _cache.Add(name, new List<GameObject>());
                _cache[name].Add(obj);
            }
        }

        obj.transform.position = Pos;
        obj.transform.rotation = Rot;
        obj.SetActive(true);
        return obj;
    }
    
    /// <summary>
    /// 将对象回收到池里
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="delayTime"></param>
    public void CollectObject(GameObject obj,float delayTime = 0f)
    {
        if (!obj) return;
        TimerMgr.Instance.CreateTimerAndStart(delayTime, 1, () => {obj.SetActive(false); });
    }

    public void ClearObject(string name)
    {
        if (_cache.ContainsKey(name))
        {
            var length = _cache[name].Count;
            for (int i = (length-1);i >= 0;i--)
            {
                GameObject.Destroy(_cache[name][i]);
            }
            _cache.Remove(name);
        }
    }
    
}
