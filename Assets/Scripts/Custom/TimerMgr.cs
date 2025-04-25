
using System;
/// <summary>
/// 管理所有计时器
/// </summary>
class TimerMgr : Singleton<TimerMgr>
{
    public event Action<float> TimerLoopCallback;

    /// <summary>
    /// 创建一个计时器并开始
    /// </summary>
    /// <param name="deltaTime">计时时间/s</param>
    /// <param name="repeatTimes">重复次数</param>
    /// <param name="callback">结束时的回调函数</param>
    /// <returns></returns>
    public Timer CreateTimerAndStart(float deltaTime, int repeatTimes, Action callback)
    {
        var _timer = CreateTimer(deltaTime, repeatTimes, callback);

        _timer.Start();

        return _timer;
    }

    public Timer CreateTimer(float deltaTime, int repeatTimes, Action callback)
    { 
        var _timer = new Timer() { DeltaTime = deltaTime, RepeatTimes = repeatTimes, Callback = callback };

        return _timer;
    }

    public void Start(Timer timer)
    {
        timer.Start();
    }

    public void Start(Timer timer, float deltaTime, int repeatTimes, Action callback)
    {
        timer.DeltaTime = deltaTime;
        timer.RepeatTimes = repeatTimes;
        timer.Callback = callback;

        timer.Start();
    }

    public void Stop(Timer timer)
    {
        timer.Stop();
    }

    public void Pause(Timer timer)
    {
        timer.Pause();
    }

    public void Loop(float deltaTime)
    {
        if (TimerLoopCallback != null)
        {
            TimerLoopCallback(deltaTime);
        }
    }

}

class Timer
{
    public float DeltaTime;
    public int RepeatTimes;
    public Action Callback;
    private float _passTime = 0;
    private int _reapeatTimes = 0;
    public bool IsRunning;

    /// <summary>
    /// 以原来时间开始
    /// </summary>
    public void Start()
    {
        if (!IsRunning)
        {
            TimerMgr.Instance.TimerLoopCallback += Loop;
            IsRunning = true;
        }
    }

    public void Pause()
    {
        if (IsRunning)
        {
            TimerMgr.Instance.TimerLoopCallback -= Loop;
            IsRunning = false;
        }
    }

    /// <summary>
    /// 重置计时器
    /// </summary>
    public void Stop()
    {
        Pause();
        
        _passTime = 0;
        _reapeatTimes = 0;

    }

    public void Loop(float deltaTime)
    {
        _passTime += deltaTime;

        if (_passTime > DeltaTime || Util.Equals(_passTime, DeltaTime))
        {
            _passTime -= DeltaTime;
            _reapeatTimes++;

            Callback();
            if(_reapeatTimes == RepeatTimes)
            {
                Stop();
            }
        }
    }
}