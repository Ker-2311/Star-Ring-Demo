using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel管理，一般在Normal层
/// </summary>
public class PanelMgr : MonoSingleton<PanelMgr>
{
    //记录当前生成的Panel
    private List<GameObject> _panelList = new List<GameObject>();
    private Stack<GameObject> _panelStack = new Stack<GameObject>();
    //是否是预加载
    private bool _isPreLoad = false;
    /// <summary>
    /// 生成一个Panel
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private GameObject LoadPanel(GameObject prefab, UIManager.UILayer uILayer)
    {
        var ui = UIManager.Instance.AddUI(prefab, uILayer);
        var panel = ui.GetComponent<BasePanel>();
        panel.Init();
        if (!_isPreLoad)
        {
            panel.OnEnter();
        }

        _panelList.Add(ui);

        return ui;
    }

    private GameObject LoadPanel(string path)
    {
        var prefab = ResMgr.Instance.GetResource<GameObject>(path);
        return LoadPanel(prefab);
    }
    private GameObject LoadPanel(GameObject prefab)
    {
        return LoadPanel(prefab, UIManager.UILayer.Normal);
    }

    /// <summary>
    /// 移除一个Panel
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    private bool RemovePanel(GameObject ui)
    {
        if (!_panelList.Exists(t => t=ui))
        {
            return false;
        }
        
        ui.GetComponent<BasePanel>().OnExit();
        UIManager.Instance.RemoveUI(ui);
        _panelList.Remove(ui);

        return true;
    }
    private bool RemovePanel(string name)
    {
        foreach (var panel in _panelList)
        {
            if (panel.name == name)
            {
                return RemovePanel(panel);
            }
        }
        return false;
    }

    /// <summary>
    /// 移除所有生成的Panel
    /// </summary>
    /// <returns></returns>
    public void RemoveAllPanel()
    {
        PopAllPanel();
        foreach(var panel in _panelList)
        {
            panel.GetComponent<BasePanel>().OnExit();
            UIManager.Instance.RemoveUI(panel);
        }
        _panelList.Clear();
    }

    /// <summary>
    /// 激活Panel
    /// </summary>
    /// <param name="panel"></param>
    private void ActivatePanel(GameObject panel)
    {
        if (_panelList.Contains(panel))
        {
            UIManager.Instance.ActivateUI(panel);
            panel.GetComponent<BasePanel>().OnEnter();
        }
    }
    /// <summary>
    /// 隐藏Panel
    /// </summary>
    /// <param name="panel"></param>
    private void HidePanel(GameObject panel)
    {
        if (PanelExists(panel) && panel.activeInHierarchy)
        {
            panel.SetActive(false);
            panel.GetComponent<BasePanel>().OnExit();
        }
    }

    /// <summary>
    /// 判断一个Panel是否已经存在
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    private bool PanelExists(GameObject ui)
    {
        if (_panelList.Contains(ui))
        {
            return true;
        }
        return false;
    }
    private bool PanelExists(string name)
    {
        foreach (var panel in _panelList)
        {
            if (panel.name == name)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取Panel
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private GameObject GetPanel(string name)
    {
        foreach (var panel in _panelList)
        {
            if (panel.name == name)
            {
                return panel;
            }
        }
        return null;
    }
    private GameObject GetPanel(GameObject ui)
    {
        return GetPanel(ui.name);
    }

    /// <summary>
    /// 打开路径为path的面板
    /// </summary>
    /// <param name="path"></param>
    private GameObject OpenPanel(GameObject ui, UIManager.UILayer uILayer)
    {
        //判断panel是否已经生成
        if (!PanelExists(ui.name))
        {
            return LoadPanel(ui, uILayer);
        }
        var panel = GetPanel(ui);
        ActivatePanel(panel);
        return panel;
    }
    private GameObject OpenPanel(GameObject ui)
    {
        return OpenPanel(ui, UIManager.UILayer.Normal);
    }
    private GameObject OpenPanel(string path)
    {
        var prefab = ResMgr.Instance.GetResource<GameObject>(path);
        return OpenPanel(prefab);
    }



    /// <summary>
    /// 将Panel推入栈
    /// </summary>
    /// <param name="ui"></param>
    public GameObject Push(GameObject ui,UIManager.UILayer uILayer)
    {
        if (_panelStack.Count != 0)
        {
            _panelStack.Peek().GetComponent<BasePanel>().OnPause();
        }
        var panel = OpenPanel(ui, uILayer);
        _panelStack.Push(panel);
        //设置panel为最前
        panel.transform.SetSiblingIndex(panel.transform.parent.childCount - 1);
        return panel;
    }
    public GameObject Push(GameObject ui)
    {
        return Push(ui, UIManager.UILayer.Normal);
    }
    public GameObject Push(string path,UIManager.UILayer uILayer)
    {
        var prefab = ResMgr.Instance.GetResource<GameObject>(path);
        return Push(prefab, uILayer);
    }
    public GameObject Push(string path)
    {
        return Push(path,UIManager.UILayer.Normal);
    }

    /// <summary>
    /// 将Panel取出栈
    /// </summary>
    /// <returns></returns>
    public GameObject Pop()
    {
        if (_panelStack.Count != 0)
        {
            var panel = _panelStack.Pop();
            HidePanel(panel);
            //恢复被覆盖面斑
            if (_panelStack.Count != 0)
            {
                _panelStack.Peek().GetComponent<BasePanel>().OnResume();
            }
            return panel;
        }
        return null;
    }

    /// <summary>
    /// 弹出所有Panel
    /// </summary>
    public void PopAllPanel()
    {
        while (_panelStack.Count > 0)
        {
            Pop();
        }
    }
    /// <summary>
    /// 获得当前栈顶Panel
    /// </summary>
    /// <returns></returns>
    public GameObject Peek()
    {
        if (_panelStack.Count > 0)
        {
            return _panelStack.Peek();
        }
        return null;
    }

    /// <summary>
    /// 预加载Panel
    /// </summary>
    /// <param name="path"></param>
    public void PreLoadPanel(string path)
    {
        StartCoroutine(PreLoad(path));
    }

    private IEnumerator PreLoad(string path)
    {
        _isPreLoad = true;
        Push(path);
        Pop();
        _isPreLoad = false;
        yield return null;
    }
}
