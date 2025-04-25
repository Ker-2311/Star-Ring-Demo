using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ռ�վ��Ϣ
/// </summary>
public class Station
{
    public string StarID;//��ϵID
    public string StationID;//�ռ�վID
    /// <summary>
    /// Key�Ǹ�������Value�ǽ���ID
    /// </summary>
    public Dictionary<string,Building> Buildings = new Dictionary<string, Building>();//�ռ�վ�����б�

    public Station(string starID)
    {
        StarID = starID;
        //�������һ���ռ�վID
        StationID = IDFactory.GenerateIdFormTime();
    }
       
}
