using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理科技
/// </summary>
public class ScienceAndTechMgr : Singleton<ScienceAndTechMgr>
{
    private Dictionary<string, Science> ScienceData
    {
        get { return DataMgr.Instance.PlayerData.ScienceData; }
        set { DataMgr.Instance.PlayerData.ScienceData = value; }
    }
    /// <summary>
    /// 初始化仅在第一次开始游戏时调用
    /// </summary>
    public void Init()
    {
        foreach(var id in ScienceTable.Instance.GetDictionary().Keys)
        {
            ScienceData.Add(id,new Science(id));
        }
        ScienceData["23891"].isUnLock = true;
        ScienceData["79626"].isUnLock = true;
        ScienceData["18954"].isUnLock = true;
    }

    /// <summary>
    /// 解锁科技
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool UnlockScience(string id)
    {
        ScienceInfo techType = ScienceTable.Instance[id];
        if (!MoneyMgr.Instance.DeductMoney(techType.CoinCost) || !IsUnlock(techType.FrontID))
        {
            //星币不够或者第一前置科技未解锁
            return false;
        }
        UnlockMgr.Instance.Push(InfoType.ResearchFinished, techType.Name);
        ScienceData[id].isUnLock = true;
        return true;
    }

    /// <summary>
    /// 判断该科技是否已激活
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsUnlock(string id)
    {
        if (id == null)
        {
            return true;
        }
        if (ScienceData[id].isUnLock)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 直接解锁科技
    /// </summary>
    /// <param name="id"></param>
    public void UnConditionUnlockScience(string id)
    {
        ScienceInfo techType = ScienceTable.Instance[id];
        UnlockMgr.Instance.Push(InfoType.ResearchFinished, techType.Name);
        ScienceData[id].isUnLock = true;
    }

    /// <summary>
    /// 直接解锁所有科技
    /// </summary>
    public void UnConditionUnlockAllScience()
    {
        foreach (var scienceid in ScienceTable.Instance.GetDictionary().Keys)
        {
            UnConditionUnlockScience(scienceid);
        }
    }

    public Dictionary<string, Science> GetScienceData()
    {
        var scienceData = new Dictionary<string, Science>();
        foreach (var data in ScienceData)
        {
            scienceData.Add(data.Key,data.Value);
        }
        return scienceData;
    }
}
