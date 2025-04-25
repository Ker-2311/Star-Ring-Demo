using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class IDFactory
{
    private static long BaseRevertTicks { get; set; }


    /// <summary>
    /// 通过时间戳生成不重复ID
    /// </summary>
    /// <param name="index">返回多少位的ID</param>
    /// <returns></returns>
    public static string GenerateIdFormTime(int index = 6)
    {
        string str;
        if (BaseRevertTicks == 0)
        {
            var now = DateTime.UtcNow.Ticks;
            var str2 = now.ToString().Reverse();
            BaseRevertTicks = long.Parse(string.Concat(str2));
        }
        BaseRevertTicks++;
        str = BaseRevertTicks.ToString();
        return str.Substring(str.Length - index);
    }

    public static string GenerateID()
    {
        return UnityEngine.Random.Range(100000, 999999).ToString();
    }
}

