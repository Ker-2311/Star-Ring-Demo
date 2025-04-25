using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��ҽ���������
/// </summary>
public class PlayerShip:IShipData
{
    public ShipInfo Info { get; set; }

    public ShipAttribute ShipAttribute { get; set; }

    /// <summary>
    /// ��װ��������Key�ǲ�λ���
    /// </summary>
    public Dictionary<string, Weapon> InstalledWeapon { get; set; } = new Dictionary<string, Weapon>();

    public Dictionary<EquipmentType, List<IEquipment>> InstalledEquipment { get; set; } = new Dictionary<EquipmentType, List<IEquipment>>();

    public PlayerShip(ShipInfo info)
    {
        Info = info;
        ShipAttribute = new ShipAttribute(info);
    }

}
