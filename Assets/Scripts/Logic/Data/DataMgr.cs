using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    public PlayerData PlayerData { get; set; }

    public void Init()
    {
        var builder = new PlayerDataBuilder();
        var director = new PlayerDataDirector();
        director.MakePlayerData(builder);
        PlayerData = builder.GetData();
    }
}
