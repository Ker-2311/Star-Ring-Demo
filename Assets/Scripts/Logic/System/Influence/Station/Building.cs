using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class Building
{
    public BuildingInfo buildingInfo;
    public int CurRank;
    //����ʣ��ʱ��
    public int buildingRemainTime;
    //����ͼ��
    public Sprite icon;
    //�Ƿ������
    public bool builded = false;
    //public bool IsLock = true;//�Ƿ�����

    public Building(string id,Sprite buildingIcon)
    {
        icon = buildingIcon;
        buildingInfo = BuildingTable.Instance[id];
        buildingRemainTime = buildingInfo.BuildingTime;
        CurRank = 1;
    }
}
