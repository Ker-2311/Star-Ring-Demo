using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ƽ�
/// </summary>
public class ScienceAndTechMgr : Singleton<ScienceAndTechMgr>
{
    private Dictionary<string, Science> ScienceData
    {
        get { return DataMgr.Instance.PlayerData.ScienceData; }
        set { DataMgr.Instance.PlayerData.ScienceData = value; }
    }
    /// <summary>
    /// ��ʼ�����ڵ�һ�ο�ʼ��Ϸʱ����
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
    /// �����Ƽ�
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool UnlockScience(string id)
    {
        ScienceInfo techType = ScienceTable.Instance[id];
        if (!MoneyMgr.Instance.DeductMoney(techType.CoinCost) || !IsUnlock(techType.FrontID))
        {
            //�ǱҲ������ߵ�һǰ�ÿƼ�δ����
            return false;
        }
        UnlockMgr.Instance.Push(InfoType.ResearchFinished, techType.Name);
        ScienceData[id].isUnLock = true;
        return true;
    }

    /// <summary>
    /// �жϸÿƼ��Ƿ��Ѽ���
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
    /// ֱ�ӽ����Ƽ�
    /// </summary>
    /// <param name="id"></param>
    public void UnConditionUnlockScience(string id)
    {
        ScienceInfo techType = ScienceTable.Instance[id];
        UnlockMgr.Instance.Push(InfoType.ResearchFinished, techType.Name);
        ScienceData[id].isUnLock = true;
    }

    /// <summary>
    /// ֱ�ӽ������пƼ�
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
