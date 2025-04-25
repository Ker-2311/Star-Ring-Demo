using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;

/// <summary>
/// �洢UI��Ϣ��������������UI
/// </summary>
public class UIManager :Singleton<UIManager>
{
    //UI����ڵ�
    private Dictionary<UILayer, GameObject> _uiLayerRoot = new Dictionary<UILayer, GameObject>();
    //UISystem���ڵ�
    private GameObject _root;
    private GameObject _canvas;
    /// <summary>
    /// ����UI���㼶��Ϣ���Ƚ����
    /// </summary>
    private Stack<BasePanel> stackPanel = new Stack<BasePanel>();

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Init()
    {
        _root = ResMgr.Instance.GetInstance("Prefabs/UI/UISystem");
        _canvas = _root.transform.Find("UICanvas").gameObject;
        GameObject.DontDestroyOnLoad(_root);
        //UI�ֲ�
        _uiLayerRoot.Add(UILayer.Scene, _canvas.transform.Find("Scene").gameObject);
        _uiLayerRoot.Add(UILayer.FightUI, _canvas.transform.Find("FightUI").gameObject);
        _uiLayerRoot.Add(UILayer.Normal, _canvas.transform.Find("Normal").gameObject);
        _uiLayerRoot.Add(UILayer.Touch, _canvas.transform.Find("Touch").gameObject);
        _uiLayerRoot.Add(UILayer.Top, _canvas.transform.Find("Top").gameObject);

        //����UIMgr��ʼ��
    }

    /// <summary>
    /// ��һ��UI����
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
    /// ɾ��UI
    /// </summary>
    /// <param name="ui"></param>
    public void RemoveUI(GameObject ui)
    {
        ResMgr.Instance.Remove(ui);
    }

    /// <summary>
    /// �Ƴ��ò�����UI
    /// </summary>
    /// <param name="uILayer"></param>
    public void RemoveLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].DestroyChilds();
    }

    /// <summary>
    /// �Ƴ����в��UI
    /// </summary>
    public void RemoveAllLayer()
    {
        foreach(var uiObject in _uiLayerRoot.Values)
        {
            uiObject.DestroyChilds();
        }
    }

    /// <summary>
    /// ���ظò�
    /// </summary>
    /// <param name="uILayer"></param>
    public void HideLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].SetActive(false);
    }

    /// <summary>
    /// ����ò�
    /// </summary>
    /// <param name="uILayer"></param>
    public void ActivateLayer(UILayer uILayer)
    {
        _uiLayerRoot[uILayer].SetActive(true);
    }

    /// <summary>
    /// ����UI
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
    /// ����UI
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
    /// �滻UI��
    /// </summary>
    public GameObject Replace(string path,UILayer uILayer)
    {
        RemoveLayer(uILayer);
        return AddUI(path, uILayer);
    }

    /// <summary>
    /// ��ȡһ�������ɵ�UI����
    /// </summary>
    /// <param name="name"></param>
    /// <param name="uILayer"></param>
    /// <returns></returns>
    public GameObject GetUI(string name,UILayer uILayer)
    {
        return _uiLayerRoot[uILayer].FindChildObject(name);
    }
    /// <summary>
    /// ��ȡCanvas��Camera
    /// </summary>
    /// <returns></returns>
    public Camera GetCamera()
    {
        return _canvas.GetComponent<Canvas>().worldCamera;
    }

    /// <summary>
    /// UI��
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
