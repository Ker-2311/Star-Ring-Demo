using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : IGameEventEffect
{
    public void EffectEnd(string parameter)
    {
        
    }

    public void EffectStart(string parameter)
    {
        Debug.Log(parameter);
    }
}
