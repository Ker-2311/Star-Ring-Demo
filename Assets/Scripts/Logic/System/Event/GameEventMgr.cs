using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventMgr : Singleton<GameEventMgr>
{

    /// <summary>
    /// �¼������ֵ�
    /// </summary>
    private Dictionary<string, Action<object[]>> _gameEventTriggerDic = new Dictionary<string, Action<object[]>>();
    /// <summary>
    /// �¼�����
    /// </summary>
    private Queue<GameEvent> _eventQueue = new Queue<GameEvent>();
    //Σ��Ϊÿ��ʱ�����һ���жϣ����ʴ���
    public int crisisTimer = 0;//Σ���ж���ʱ��
    //private int _crisisDecideTime = 10;//ÿ10���ж�һ��Σ��
    private GameObject _eventPanelPrefab;
    //�¼�Panel�Ƿ��Ѿ���
    public bool panelIsOpen = false;

    public void Init()
    {
        _eventPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/EventPanel");

        //��ʼ���¼��ֵ�
        foreach (var gameEventInfo in EventTable.Instance.GetDictionary().Values)
        {
            var gameEvent = new GameEvent(gameEventInfo);
            //����ֵ�û�д����¼�����
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
        //�¼���������Ч���ֵ��ʼ��
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
    /// ������ʣ��ж��Ƿ񴥷��¼�
    /// </summary>
    /// <param name="eventID"></param>
    public void EventDecide(string eventID)
    {
        var eventInfo = EventTable.Instance[eventID];
        var gameEvent = Data.Instance.GameEventData[eventInfo.EventType][eventID];

        EventTrigger(eventID);
    }

    /// <summary>
    /// �����¼�
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
    /// ��ʾ�¼�,�����˵�Update����
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
