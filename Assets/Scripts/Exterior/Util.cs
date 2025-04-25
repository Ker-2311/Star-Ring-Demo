using System;
using System.Collections.Generic;
using UnityEngine;

static class Util
{ 
    /// <summary>
    /// 角度转换弧度制
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    static public float AngleToRadians(float angle)
    {
        return angle * Mathf.PI / 180;
    }

    /// <summary>
    /// 判断浮点数相等
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    static public bool FloatEqual(float a,float b)
    {
        if (Mathf.Abs(b - a) <= 0.0001f)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 角度制三角函数
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    static public float RadiansCos(float angle)
    {
        return Mathf.Cos(AngleToRadians(angle));
    }
    static public float RadiansSin(float angle)
    {
        return Mathf.Sin(AngleToRadians(angle));
    }
    /// <summary>
    /// 随机生成一个int
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="condition">生成符合该条件的数</param>
    /// <returns></returns>
    static public int RandomGenerateInt(int a,int b,Func<int,bool> condition = null)
    {
        int MaxCount = 50000;
        while (MaxCount >= 0)
        {
            int count = UnityEngine.Random.Range(a, b);
            if (condition != null && condition(count))
            {
                return count;
            }
            MaxCount--;
        }
        Debug.LogError("ID生成循环超出最大上线");
        return 0;
    }



}
