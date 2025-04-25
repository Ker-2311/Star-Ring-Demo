using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force
{
    public ForceInfo forceInfo;
    public string playerIcon;

    /// <summary>
    /// 生成玩家势力
    /// </summary>
    /// <param name="name"></param>
    public Force(string name)
    {
        forceInfo = new ForceInfo();
        forceInfo.ID = "000000";
        forceInfo.Name = name;
        playerIcon = "0 0 0 0 0 0";
    }

    public Force(ForceInfo info)
    {
        forceInfo = info;
    }
}
