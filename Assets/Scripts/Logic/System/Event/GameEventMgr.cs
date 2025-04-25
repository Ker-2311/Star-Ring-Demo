using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventMgr : Singleton<GameEventMgr>
{

    /// <summary>
    /// 事件触发字典
    /// </summary>
    private Dictionary<string, Action<object[]>> _gameEventTriggerDic = new Dictionary<string, Action<object[]>>();
    /// <summary>
    /// 事件队列
    /// </summary>
    private Queue<GameEvent> _eventQueue = new Queue<GameEvent>();
    //危机为每段时间进行一次判断，概率触发
    public int crisisTimer = 0;//危机判定记时器
    //private int _crisisDecideTime = 10;//每10天判断一次危机
    private GameObject _eventPanelPrefab;
    //事件Panel是否已经打开
    public bool panelIsOpen = false;

    public void Init()
    {
        _eventPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/EventPanel");

        //初始化事件字典
        foreach (var gameEventInfo in EventTable.Instance.GetDictionary().Values)
        {
            var gameEvent = new GameEvent(gameEventInfo);
            //如果字典没有创建事件类型
            if (!Data.Instance.GameEventData.ContainsKey(gameEventInfo.EventType))
            {
                var dic = new Dictionary<string, GameEvent>();
                dic.Add(gameEventInfo.ID, gameEvent);
                Data.Instance.GameEventData.Add(gameEventInfo.EventType, dic);
            }
            else
            {
                Data.Instance.GameEventData[gameEventInfo.EventType].Add(gameEventInfo.ID, gameEvent);
            }
        }
        //事件触发器和效果字典初始化
        Type trigger = typeof(GameEventTrigger);
        var triggerMethods = trigger.GetMethods();
        foreach (var method in triggerMethods)
        {
            if (method.ReturnType == typeof(bool))
            {
                _gameEventTriggerDic.Add(method.Name, (object[] par) => method.Invoke(null, par));
            }
        }
    }

    /// <summary>
    /// 计算概率，判定是否触发事件
    /// </summary>
    /// <param name="eventID"></param>
    public void EventDecide(string eventID)
    {
        var eventInfo = EventTable.Instance[eventID];
        var gameEvent = Data.Instance.GameEventData[eventInfo.EventType][eventID];

        EventTrigger(eventID);
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventID"></param>
    public void EventTrigger(string eventID)
    {
        var eventInfo = EventTable.Instance[eventID];
        var gameEvent = Data.Instance.GameEventData[eventInfo.EventType][eventID];
        _eventQueue.Enqueue(gameEvent);
        Data.Instance.TriggerEventData.Add(gameEvent);
    }

    /// <summary>
    /// 显示事件,在主菜单Update里检测
    /// </summary>
    /// <returns></returns>
    public GameEvent ShowEvent()
    {
        if (_eventQueue.Count > 0 && !panelIsOpen)
        {
            var gameEvent = _eventQueue.Dequeue();
            PanelMgr.Instance.Push(_eventPanelPrefab,UIManager.UILayer.Top).GetComponent<EventPanel>().CurEvent = gameEvent;
            return gameEvent;
        }
        return null;
    }
}
