using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcesMgr : Singleton<SourcesMgr>
{
    private Dictionary<string, Source> SourcesData
    {
        get { return DataMgr.Instance.PlayerData.SourcesData; }
        set { DataMgr.Instance.PlayerData.SourcesData = value; }
    }

    /// <summary>
    /// 增加资源数量
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void IncreaseSource(string id,int count)
    {
        if (SourcesData[id].count == 0)
        {

        }
        SourcesData[id].count += count;
    }

    /// <summary>
    /// 获取当前资源列表(只读)
    /// </summary>
    /// <returns></returns>
    public Dictionary<string,Source> GetSourcesDic()
    {
        var dic = new Dictionary<string, Source>();
        foreach (var source in SourcesData.Values)
        {
            dic.Add(source.sourceInfo.ID,source);
        }
        return dic;
    }

}
