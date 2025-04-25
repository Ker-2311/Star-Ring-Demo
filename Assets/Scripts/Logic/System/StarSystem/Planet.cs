using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    //星球类型序号
    public int PlanetTypeIndex;
    //星球轨道序号从0-6
    public int RailIndex;
    //星球运行角度
    public float Angle;
    //上次进入星历日
    public int LastEnterDays;
    //星球自转速度

    public Planet()
    {
        LastEnterDays = 0;
        //注意该处逻辑应改
        PlanetTypeIndex = Random.Range(0, 10);
    }
}
