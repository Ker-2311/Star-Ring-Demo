using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : Singleton<T>, new()
{

    private static T _instance = new T();

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

}

