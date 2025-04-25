using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockInfo
{
    public string infoStr;
    public InfoType infoType;
}

public class UnlockMgr : Singleton<UnlockMgr>
{
    private GameObject _unlockPanelPrefab;
    private GameObject _unlockPanel;
    private Stack<UnlockInfo> _infoStack;

    public void Init()
    {
        _infoStack = new Stack<UnlockInfo>();
        _unlockPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/Main/UnLockPanel");
    }

    public void Push(InfoType infoType, string infoStr)
    {
        var info = new UnlockInfo() { infoType = infoType, infoStr = infoStr };
        if (_unlockPanel == null)
        {
            _unlockPanel = UIManager.Instance.AddUI(_unlockPanelPrefab, UIManager.UILayer.Top);
        }
        _infoStack.Push(info);
        ShowInfo();
        _unlockPanel.SetActive(true);
    }

    public void Pop()
    {
        if (_infoStack.Count != 0)
        {
            _infoStack.Pop();
            if (_infoStack.Count == 0)
            {
                GameObject.Destroy(_unlockPanel);
                _unlockPanel = null;
            }
            else
            {
                ShowInfo();
            }
        }
    }

    private void ShowInfo()
    {
        var info = _infoStack.Peek();
        var text = _unlockPanel.transform.Find("Text").GetComponent<Text>();
        switch(info.infoType)
        {
            case InfoType.BuildingFinished: text.text = "建筑建造完成:" + info.infoStr; break;
            case InfoType.ResearchFinished: text.text = "科技研究完成:" + info.infoStr; break;
            case InfoType.PsionicLevelUp: text.text = "灵能升级"; break;
        }
    }
}
public enum InfoType
{
    BuildingFinished,
    ResearchFinished,
    PsionicLevelUp,
}