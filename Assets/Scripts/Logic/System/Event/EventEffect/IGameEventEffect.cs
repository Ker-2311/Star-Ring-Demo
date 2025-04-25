using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEventEffect
{
    /// <summary>
    /// 事件效果开始
    /// </summary>
    /// <param name="gameEvent"></param>
    public void EffectStart(string parameter);
    /// <summary>
    /// 事件效果结束
    /// </summary>
    /// <param name="gameEvent"></param>
    public void EffectEnd(string parameter);
}
