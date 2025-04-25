using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMgr : Singleton<PlayerShipMgr>
{
    private PlayerShip ActivePlayerShip
    {
        get { return DataMgr.Instance.PlayerData.ActivePlayerShip; }
        set { DataMgr.Instance.PlayerData.ActivePlayerShip = value; }
    }

    public void ActivateShip(PlayerShip playerShip)
    {
        ActivePlayerShip = playerShip;
    }

    /// <summary>
    /// ��װ������ָ����λ
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="index"></param>
    public void InstallWeapon(Weapon weapon,string index)
    {
        ActivePlayerShip.InstalledWeapon[index] = weapon;
    }

    /// <summary>
    /// �Ƴ�ָ����λ����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool RemoveWeapon(string index)
    {
        if (ActivePlayerShip.InstalledWeapon.ContainsKey(index))
        {
            ActivePlayerShip.InstalledWeapon.Remove(index);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ���ָ����λ����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Weapon GetWeapon(string index)
    {
        ActivePlayerShip.InstalledWeapon.TryGetValue(index, out var weapon);
        return weapon;
    }

    /// <summary>
    /// ��װһ��װ��
    /// </summary>
    /// <param name="equipment"></param>
    public void InstallEquipment(IEquipment equipment)
    {
        ActivePlayerShip.InstalledEquipment[equipment.EquipmentType].Add(equipment);
    }

    /// <summary>
    /// ж��һ��װ��
    /// </summary>
    /// <param name="equipment"></param>
    public bool RemoveEquipment(IEquipment equipment)
    {
        if (ActivePlayerShip.InstalledEquipment[equipment.EquipmentType].Contains(equipment))
        {
            ActivePlayerShip.InstalledEquipment[equipment.EquipmentType].Remove(equipment);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ��ȡ�Ѱ�װװ���б�
    /// </summary>
    /// <returns></returns>
    public List<T> GetInstalledEquipmentList<T>(EquipmentType type) where T : class
    {
        List<T> list = new List<T>();
        ActivePlayerShip.InstalledEquipment[type].ForEach((x) => list.Add(x as T));
        return list;
    }

    public List<Weapon> GetInstalledWeaponList()
    {
        List<Weapon> list = new List<Weapon>();
        foreach (var value in ActivePlayerShip.InstalledWeapon.Values)
        {
            list.Add(value);
        }
        return list;
    }

    public PlayerShip GetActivateShip()
    {
        return ActivePlayerShip;
    }
}
