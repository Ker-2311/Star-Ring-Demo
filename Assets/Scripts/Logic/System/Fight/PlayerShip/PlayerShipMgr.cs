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
    /// 安装武器到指定槽位
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="index"></param>
    public void InstallWeapon(Weapon weapon,string index)
    {
        ActivePlayerShip.InstalledWeapon[index] = weapon;
    }

    /// <summary>
    /// 移除指定槽位武器
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
    /// 获得指定槽位武器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Weapon GetWeapon(string index)
    {
        ActivePlayerShip.InstalledWeapon.TryGetValue(index, out var weapon);
        return weapon;
    }

    /// <summary>
    /// 安装一个装备
    /// </summary>
    /// <param name="equipment"></param>
    public void InstallEquipment(IEquipment equipment)
    {
        ActivePlayerShip.InstalledEquipment[equipment.EquipmentType].Add(equipment);
    }

    /// <summary>
    /// 卸下一个装备
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
    /// 获取已安装装备列表
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
