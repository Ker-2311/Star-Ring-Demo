using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEventEffect
{
    /// <summary>
    /// �¼�Ч����ʼ
    /// </summary>
    /// <param name="gameEvent"></param>
    public void EffectStart(string parameter);
    /// <summary>
    /// �¼�Ч������
    /// </summary>
    /// <param name="gameEvent"></param>
    public void EffectEnd(string parameter);
}
