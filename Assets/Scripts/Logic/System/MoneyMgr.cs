using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMgr : Singleton<MoneyMgr>
{
    private int Money { get { return DataMgr.Instance.PlayerData.MoneyData; } set { DataMgr.Instance.PlayerData.MoneyData = value; } }

    /// <summary>
    /// Ôö¼ÓÐÇ±Ò
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseMoney(int count)
    {
        Money += count;
    }

    /// <summary>
    /// ¿Û³ýÐÇ±Ò
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool DeductMoney(int count)
    {
        if (Money >= count)
        {
            Money -= count;
            return true;
        }
        return false;
    }
}
