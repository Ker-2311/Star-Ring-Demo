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
    /// 设置默认绑定按键
    /// </summary>
    public void Setup()
    {
        //BlindMouseEvent(MouseEventStatus.MainPanel,0,)
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="status"></param>
    public void ChangeStatus(MouseEventStatus status)
    {
        _status = status;
    }

    /// <summary>
    /// 绑定鼠标事件
    /// </summary>
    /// <param name="status"></param>
    /// <param name="key">表示鼠标按键指数,0左键，1右键，2中键</param>
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
    /// 监听用户输入，挂载到GameEngine
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
