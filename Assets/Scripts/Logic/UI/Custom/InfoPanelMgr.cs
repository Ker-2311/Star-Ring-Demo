using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelMgr : Singleton<InfoPanelMgr>
{
    private GameObject _infoPanelPrefab;
    private GameObject _infoPanel;
    private Vector2 _infoPanelPos;
    public bool IsShowStatue = false;

    public void Init()
    {
        _infoPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/InfoPanel");
    }

    public void UpdatePosition()
    {
        if (_infoPanel != null && IsShowStatue)
        {
            _infoPanel.transform.GetComponent<RectTransform>().anchoredPosition = _infoPanelPos;
        }
    }

    public void ShowInfoPanel(string info,Transform parent,Vector2 pos)
    {
        if (_infoPanel == null)
        {
            _infoPanel = ResMgr.Instance.GetInstance(_infoPanelPrefab, parent);
        }
        var text = _infoPanel.transform.Find("Text").GetComponent<Text>();
        IsShowStatue = true;
        text.text = info;
        _infoPanelPos = pos;
        //���ó�����Ļ��Ե�Զ�����
        //RectTransform rect = _infoPanel.transform.GetComponent<RectTransform>();
        //float width = rect.sizeDelta.x;
        //float height = rect.sizeDelta.y;

        //Vector2 pivot = new Vector2();

        //if (pos.x + width <= Screen.width) // ���ȿ���
        //{
        //    pivot.x = 0;
        //}
        //else // ��
        //{
        //    pivot.x = 1;
        //}

        //if (pos.y - height >= 0) // ���ȿ���
        //{
        //    pivot.y = 1;
        //}
        //else // ��
        //{
        //    pivot.y = 0;
        //}
        //rect.pivot = pivot;
    }

    public void CloseInfoPanel()
    {
        if (_infoPanel != null)
        {
            GameObject.Destroy(_infoPanel);
            IsShowStatue = false;
            _infoPanel = null;
        }
    }
}
