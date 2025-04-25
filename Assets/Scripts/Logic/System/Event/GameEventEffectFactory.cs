using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventEffectFactory
{
    private static Dictionary<string, object> _cache;
    static GameEventEffectFactory()
    {
        _cache = new Dictionary<string, object>();
    }

    private static T CreateObject<T>(string className) where T : class
    {
        if (!_cache.ContainsKey(className))
        {
            Type type = Type.GetType(className);
            object instance = Activator.CreateInstance(type);
            _cache.Add(className, instance);
        }
        return _cache[className] as T;
    }

    public static IGameEventEffect CreateGameEventEffect(string optionEffect)
    {
        return CreateObject<IGameEventEffect>(optionEffect);
    }
}
