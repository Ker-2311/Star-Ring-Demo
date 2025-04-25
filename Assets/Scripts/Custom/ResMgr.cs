using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResMgr : Singleton<ResMgr>
{
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T GetResource<T>(string path) where T: UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 加载AssetBundle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string path,string name) where T: UnityEngine.Object
    {
        var ab = AssetBundle.LoadFromFile(path);
        return ab.LoadAsset<T>(name);
    }

    /// <summary>
    /// 加载所有类型为T的AB包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T[] LoadAllAssets<T>(string path) where T:UnityEngine.Object
    {
        var ab = AssetBundle.LoadFromFile(path);
        return ab.LoadAllAssets<T>();
    }

    public T[] GetAllResources<T>(string path) where T: UnityEngine.Object
    {
        return Resources.LoadAll<T>(path);
    }

    /// <summary>
    /// 获得实例
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject GetInstance(string path)
    {
        var instance = GameObject.Instantiate(GetResource<GameObject>(path));
        instance.name = instance.name.Substring(0, (instance.name.LastIndexOf("(")));
        return instance;
    }
    public GameObject GetInstance(string path,string name)
    {
        var instance = GameObject.Instantiate(GetResource<GameObject>(path));
        instance.name = name;
        return instance;
    }
    public GameObject GetInstance(string path, string name,Transform parent,bool IsResetWorldPosition = true)
    {
        var instance = GameObject.Instantiate(GetResource<GameObject>(path), parent, !IsResetWorldPosition);
        instance.name = name;
        return instance;
    }
    public GameObject GetInstance(GameObject gameObject,Transform parent = null, bool IsResetWorldPosition = true)
    {
        var instance = GameObject.Instantiate(gameObject, parent, !IsResetWorldPosition);
        instance.name = instance.name.Substring(0, (instance.name.LastIndexOf("(")));
        return instance;
    }

    public void Remove(GameObject ui)
    {
        GameObject.Destroy(ui);
    }
}
