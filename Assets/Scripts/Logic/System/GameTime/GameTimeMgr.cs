using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 游戏时间管理,30天1月，12月1年
/// </summary>
public class GameTimeMgr : Singleton<GameTimeMgr>
{
    private GameTime TimeData { get { return DataMgr.Instance.PlayerData.TimeData; } set { DataMgr.Instance.PlayerData.TimeData = value; } }
    //游戏时间流
    private List<GameTimeStream> _gameTimeStream = new List<GameTimeStream>();
    private float _timeSpeed = 1;
    //游戏每天的秒数
    private const int daySec = 2;
    private Timer timer;
    public void Init()
    {
        timer = TimerMgr.Instance.CreateTimerAndStart(daySec / _timeSpeed, -1, DayPlus);
    }

    /// <summary>
    /// 游戏时间开始
    /// </summary>
    public void StartTime()
    {
        TimerMgr.Instance.Start(timer);
    }
    
    /// <summary>
    /// 获取星历日
    /// </summary>
    public int GetDays()
    {
        return (TimeData.Days + (TimeData.Mouths * 30) + (TimeData.Years * 365));
    }

    /// <summary>
    /// 游戏时间暂停
    /// </summary>
    public void PauseTime()
    {
        TimerMgr.Instance.Pause(timer);
    }

    /// <summary>
    /// 将天数转换为GameTime
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
    /// 获取当前游戏时间
    /// </summary>
    /// <returns></returns>
    public GameTime GetGameTime()
    {
        return TimeData;
    }

    /// <summary>
    /// 游戏天+1
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
    /// 添加游戏时间流
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
    /// 检测并更新时间流
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
