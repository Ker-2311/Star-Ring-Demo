using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;

/// <summary>
/// 存储UI信息，创建或者销毁UI
/// </summary>
public class UIManager :Singleton<UIManager>
{
    //UI层根节点
    private Dictionary<UILayer, GameObject> _uiLayerRoot = new Dictionary<UILayer, GameObject>();
    //UISystem根节点
    private GameObject _root;
    private GameObject _canvas;
    /// <summary>
    /// 保存UI面板层级信息，先进后出
    /// </summary>
    private Stack<BasePanel> stackPanel = new Stack<BasePanel>();

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        _root = ResMgr.Instance.GetInstance("Prefabs/UI/UISystem");
        _canvas = _root.transform.Find("UICanvas").gameObject;
        GameObject.DontDestroyOnLoad(_root);
        //UI分层
        _uiLayerRoot.Add(UILayer.Scene, _canvas.transform.Find("Scene").gameObject);
        _uiLayerRoot.Add(UILayer.FightUI, _canvas.transform.Find("FightUI").gameObject);
        _uiLayerRoot.Add(UILayer.Normal, _canvas.transform.Find("Normal").gameObject);
        _uiLayerRoot.Add(UILayer.Touch, _canvas.transform.Find("Touch").gameObject);
        _uiLayerRoot.Add(UILayer.Top, _canvas.transform.Find("Top").gameObject);

        //各层UIMgr初始化
    }

    /// <summary>
    /// 将一个UI生成
    /// </summary>
    /// <param name="path"></param>
    /// <param name="uILayer"></param>
    /// <returns></returns>
    public GameObject AddUI(string path,UILayer uILayer)
    {
        var root = ResMgr.Instance.GetInstance(path);
        if (root != null)
        {
            root.transform.SetParent(_uiLayerRoot[uILayer].transform, false);
        }
        return root;
    }
    public GameObject AddUI(GameObject ui, UILayer uILayer)
    {
        var root = ResMgr.Instance.GetInstance(ui);
        if (root != null)
        {
            root.transform.SetParent(_uiLayerRoot[uILayer].transform, false);
        }
        return root;
    }

    /// <summary>
    /// 删除UI
    /// </summary>
    /// <param name="ui"></param>
    public void RemoveUI(GameObject ui)
    {
        ResMgr.Instance.Remove(ui);
    }

    /// <summary>
    /// 移除该层所有UI
    /// </summary>
    /// <param name="uILayer"></param>
    public void RemoveLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].DestroyChilds();
    }

    /// <summary>
    /// 移除所有层的UI
    /// </summary>
    public void RemoveAllLayer()
    {
        foreach(var uiObject in _uiLayerRoot.Values)
        {
            uiObject.DestroyChilds();
        }
    }

    /// <summary>
    /// 隐藏该层
    /// </summary>
    /// <param name="uILayer"></param>
    public void HideLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].SetActive(false);
    }

    /// <summary>
    /// 激活该层
    /// </summary>
    /// <param name="uILayer"></param>
    public void ActivateLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].SetActive(true);
    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    /// <param name="ui"></param>
    public void HideUI(GameObject ui)
    {
        if (ui != null)
        {
            ui.SetActive(false);
        }
    }

    /// <summary>
    /// 激活UI
    /// </summary>
    /// <param name="ui"></param>
    public void ActivateUI(GameObject ui)
    {
        if (ui != null)
        {
            ui.SetActive(true);
        }
    }

    /// <summary>
    /// 替换UI层
    /// </summary>
    public GameObject Replace(string path,UILayer uILayer)
    {
        RemoveLayer(uILayer);
        return AddUI(path, uILayer);
    }

    /// <summary>
    /// 获取一个已生成的UI对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="uILayer"></param>
    /// <returns></returns>
    public GameObject GetUI(string name,UILayer uILayer)
    {
        return _uiLayerRoot[uILayer].FindChildObject(name);
    }
    /// <summary>
    /// 获取Canvas的Camera
    /// </summary>
    /// <returns></returns>
    public Camera GetCamera()
    {
        return _canvas.GetComponent<Canvas>().worldCamera;
    }

    /// <summary>
    /// UI层
    /// </summary>
    public enum UILayer
    {
        Scene,
        Touch,
        FightUI,
        Normal,
        Top,
    }

}
