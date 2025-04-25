using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardEventBinding:Singleton<KeyboardEventBinding>
{
    private KeyboardStatus _status;
    public Dictionary<KeyboardStatus, Dictionary<KeyCode, Action>> bindingDictionary;
    
    public void Init()
    {
        bindingDictionary = new Dictionary<KeyboardStatus, Dictionary<KeyCode, Action>>();
        Setup();
    }

    /// <summary>
    /// ����Ĭ�ϰ󶨰���
    /// </summary>
    public void Setup()
    {

    }

    /// <summary>
    /// �ı�״̬
    /// </summary>
    /// <param name="status"></param>
    public void ChangeStatus(KeyboardStatus status)
    {
        _status = status;
    }

    /// <summary>
    /// �󶨼����¼�
    /// </summary>
    /// <param name="status"></param>
    /// <param name="key"></param>
    /// <param name="action"></param>
    public void BindKeyboardEvent(KeyboardStatus status, KeyCode key,Action action)
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
            bindingDictionary.Add(status, new Dictionary<KeyCode, Action>());
            bindingDictionary[status].Add(key, action);
        }
    }

    /// <summary>
    /// �����û����룬���ص�GameEngine
    /// </summary>
    public void DetectionInput()
    {
        if (bindingDictionary.ContainsKey(KeyboardStatus.Global))
        {
            //ȫ�ְ������ 
            foreach (var bindingActions in bindingDictionary[KeyboardStatus.Global])
            {
                if (Input.GetKeyDown(bindingActions.Key))
                {
                    bindingActions.Value();
                    return;
                }
            }
        }
        if (bindingDictionary.ContainsKey(_status))
        {
            //��ǰ״̬�������
            foreach (var bindingActions in bindingDictionary[_status])
            {
                if (Input.GetKeyDown(bindingActions.Key))
                {
                    bindingActions.Value();
                    return;
                }
            }
        }
    }
    public enum KeyboardStatus
    {
        Global,//ȫ�ְ���
        MainPanel,
        Fight
    }
}