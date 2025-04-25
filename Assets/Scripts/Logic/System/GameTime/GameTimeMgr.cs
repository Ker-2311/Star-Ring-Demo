using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Ϸʱ�����,30��1�£�12��1��
/// </summary>
public class GameTimeMgr : Singleton<GameTimeMgr>
{
    private GameTime TimeData { get { return DataMgr.Instance.PlayerData.TimeData; } set { DataMgr.Instance.PlayerData.TimeData = value; } }
    //��Ϸʱ����
    private List<GameTimeStream> _gameTimeStream = new List<GameTimeStream>();
    private float _timeSpeed = 1;
    //��Ϸÿ�������
    private const int daySec = 2;
    private Timer timer;
    public void Init()
    {
        timer = TimerMgr.Instance.CreateTimerAndStart(daySec / _timeSpeed, -1, DayPlus);
    }

    /// <summary>
    /// ��Ϸʱ�俪ʼ
    /// </summary>
    public void StartTime()
    {
        TimerMgr.Instance.Start(timer);
    }
    
    /// <summary>
    /// ��ȡ������
    /// </summary>
    public int GetDays()
    {
        return (TimeData.Days + (TimeData.Mouths * 30) + (TimeData.Years * 365));
    }

    /// <summary>
    /// ��Ϸʱ����ͣ
    /// </summary>
    public void PauseTime()
    {
        TimerMgr.Instance.Pause(timer);
    }

    /// <summary>
    /// ������ת��ΪGameTime
    /// </summary>
    /// <returns></returns>
    public GameTime DayToGameTime(int days)
    {
        var time = new GameTime();
        time.Years = days / 360;
        time.Mouths = (days - time.Years * 360) / 30;
        time.Days = (days - time.Years * 360 - time.Mouths*30);
        return time;
    }

    /// <summary>
    /// ��ȡ��ǰ��Ϸʱ��
    /// </summary>
    /// <returns></returns>
    public GameTime GetGameTime()
    {
        return TimeData;
    }

    /// <summary>
    /// ��Ϸ��+1
    /// </summary>
    private void DayPlus()
    {
        TimeData.Days++;
        if(TimeData.Days >= 31)
        {
            TimeData.Days -= 30;
            TimeData.Mouths++;
        }
        if (TimeData.Mouths >= 13)
        {
            TimeData.Mouths -= 12;
            TimeData.Years++;
        }
        DetectTimeStraem();
    }

    /// <summary>
    /// �����Ϸʱ����
    /// </summary>
    /// <param name="days"></param>
    /// <param name="action"></param>
    public void AddTimeStream(int count, int days,Action action)
    {
        if (days > 0)
        {
            var stream = new GameTimeStream(action, days, count);
            _gameTimeStream.Add(stream);
        }
        else
        {
            action();
        }
    }

    /// <summary>
    /// ��Ⲣ����ʱ����
    /// </summary>
    private void DetectTimeStraem()
    {
        for (int i =0;i<_gameTimeStream.Count;i++)
        {
            var stream = _gameTimeStream[i];
            stream.remainTime--;
            if (stream.remainTime <= 0)
            {
                stream.action();
                if (stream.runCount > 0)
                {
                    stream.runCount--;
                    stream.remainTime = stream.originTime; 
                }
                else if (stream.runCount == 0)
                {
                    _gameTimeStream.Remove(stream);
                }
            }
        }
    }

    public void AccelerateTime(float timeSpeed)
    {
        throw new NotImplementedException();
    }
}
