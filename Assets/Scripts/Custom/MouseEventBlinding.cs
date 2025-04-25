using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseEventBlinding : Singleton<MouseEventBlinding>
{
    private MouseEventStatus _status;
    public Dictionary<MouseEventStatus, Dictionary<int, Action>> bindingDictionary;

    public void Init()
    {
        bindingDictionary = new Dictionary<MouseEventStatus, Dictionary<int, Action>>();
        Setup();
    }

    /// <summary>
    /// ����Ĭ�ϰ󶨰���
    /// </summary>
    public void Setup()
    {
        //BlindMouseEvent(MouseEventStatus.MainPanel,0,)
    }

    /// <summary>
    /// �ı�״̬
    /// </summary>
    /// <param name="status"></param>
    public void ChangeStatus(MouseEventStatus status)
    {
        _status = status;
    }

    /// <summary>
    /// ������¼�
    /// </summary>
    /// <param name="status"></param>
    /// <param name="key">��ʾ��갴��ָ��,0�����1�Ҽ���2�м�</param>
    /// <param name="action"></param>
    public void BlindMouseEvent(MouseEventStatus status, int key, Action action)
    {
        if (bindingDictionary.ContainsKey(status))
        {
            if (bindingDictionary[status].ContainsKey(key))
            {
                bindingDictionary[status][key] = action;
            }
            else
            {
                bindingDictionary[status].Add(key, action);
            }
        }
        else
        {
            bindingDictionary.Add(status, new Dictionary<int, Action>());
            bindingDictionary[status].Add(key, action);
        }
    }

    /// <summary>
    /// �����û����룬���ص�GameEngine
    /// </summary>
    public void DetectionInput()
    {
        if (bindingDictionary.Count != 0 && bindingDictionary.ContainsKey(_status))
        {
            foreach (var bindingActions in bindingDictionary[_status])
            {
                if (Input.GetMouseButtonDown(bindingActions.Key))
                {
                    bindingActions.Value();
                }
            }
        }
    }
    public enum MouseEventStatus
    {
        MainPanel,
        ResearchPanel,
        Fight
    }
}
