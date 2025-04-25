using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMgr : Singleton<ForceMgr>
{
    public void Init()
    {
        AddPlayerForce("Player");
    }


    public Force AddPlayerForce(string name)
    {
        var force = new Force(name);
        Data.Instance.ForceData.Add(force.forceInfo.ID, force);
        return force;
    }

    /// <summary>
    /// ��ȡ���������������
    /// </summary>
    /// <returns></returns>
    public List<Force> GetAllForce()
    {
        List<Force> forces = new List<Force>();
        foreach (var force in Data.Instance.ForceData.Values)
        {
            forces.Add(force);
        }
        forces.Sort((x, y) => Convert.ToInt32(x.forceInfo.ID).CompareTo(Convert.ToInt32(y.forceInfo.ID)));
        return forces;
    }

    /// <summary>
    /// ��ȡ�������
    /// </summary>
    /// <returns></returns>
    public Force GetPlayerForce()
    {
        if (Data.Instance.ForceData.ContainsKey("000000"))
        {
            return Data.Instance.ForceData["000000"];
        }
        return null;
    }

}
