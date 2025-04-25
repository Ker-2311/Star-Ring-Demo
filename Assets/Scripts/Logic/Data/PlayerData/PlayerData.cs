using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData:Data
{
    //�Ǳ�
    public int MoneyData { get; set; }

    //ʱ��
    public GameTime TimeData { get; set; }

    //�������
    public Dictionary<string, Item> InventoryData { get; set; }
    //�ֿ�����
    public Dictionary<string, Item> WareHouseData { get; set; }
    //��Դ����
    public Dictionary<string, Source> SourcesData { get; set; }
    // �Ƽ�����,�������ѽ����������о��ĿƼ�
    public Dictionary<string, Science> ScienceData { get; set; }
    //��ϵ����,���������ϵ
    public Dictionary<string, Star> StarData { get; set; }

    //������������
    public List<string> BuildingLockData { get; set; }
    //�ռ�վ����, �����ѽ����Ŀռ�վ��Ϣ
    public Dictionary<string, Station> StationData { get; set; }
    //��������
    public Dictionary<string, Force> ForceData { get; set; }

    //װ������
    public Dictionary<string, IEquipment> EquipmentData { get; set; } 

    /// <summary>
    /// ���������н�������
    /// </summary>
    public List<PlayerShip> PlayerShipsData { get; set; }

    //������ڼ���Ľ���
    public PlayerShip ActivePlayerShip { get; set; }

    public Dictionary<string,NPCShip> NPCShipsData { get; set; }
}
