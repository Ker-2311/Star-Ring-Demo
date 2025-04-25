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
    /// 设置默认绑定按键
    /// </summary>
    public void Setup()
    {

    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="status"></param>
    public void ChangeStatus(KeyboardStatus status)
    {
        _status = status;
    }

    /// <summary>
    /// 绑定键盘事件
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
    /// 监听用户输入，挂载到GameEngine
    /// </summary>
    public void DetectionInput()
    {
        if (bindingDictionary.ContainsKey(KeyboardStatus.Global))
        {
            //全局按键检测 
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
            //当前状态按键检测
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
        Global,//全局按键
        MainPanel,
        Fight
    }
}