using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataBuilder : DataBuilder<PlayerData>
{
    private PlayerData _playerData;

    public PlayerDataBuilder()
    {
        _playerData = new PlayerData();
    }

    public void InitMoneyData()
    {
        _playerData.MoneyData = 0;
    }

    public void InitTimeData()
    {
        _playerData.TimeData = new GameTime() { Years = 9200, Mouths = 2, Days = 1 };
    }

    public void InitInventoryData()
    {
        _playerData.InventoryData = new Dictionary<string, Item>();
    }

    public void InitSourcesData()
    {
        _playerData.SourcesData = new Dictionary<string, Source>();
        foreach (var source in SourcesTable.Instance.GetDictionary())
        {
            _playerData.SourcesData.Add(source.Key, new Source(source.Value));
        }
    }

    public void InitScienceData()
    {
        _playerData.ScienceData = new Dictionary<string, Science>();
    }
    public void InitStarData()
    {
        _playerData.StarData = new Dictionary<string, Star>();
    }

    public void InitBuildingLockData()
    {
        _playerData.BuildingLockData = new List<string>();
    }

    public void InitStationData()
    {
        _playerData.StationData = new Dictionary<string, Station>();
    }

    public void InitForceData()
    {
        _playerData.ForceData = new Dictionary<string, Force>();
    }
    public void InitEquipmentData()
    {
        _playerData.EquipmentData = new Dictionary<string, IEquipment>();
        //加载武器数据
        foreach (var config in WeaponTable.Instance.GetDictionary())
        {
            var equipment = new Weapon();
            equipment.IsLock = true;
            equipment.WeaponInfo = config.Value;
            _playerData.EquipmentData.Add(config.Key, equipment);
        }
    }

    public void InitPlayerShipsData()
    {
        _playerData.PlayerShipsData = new List<PlayerShip>();
        //添加灵能护卫舰
        var ship = new PlayerShip(ShipTable.Instance["283453"]);
        _playerData.PlayerShipsData.Add(ship);
    }

    public void InitActivePlayerShip()
    {
        _playerData.ActivePlayerShip = _playerData.PlayerShipsData.First();
    }

    public void InitNPCShipsData()
    {
        _playerData.NPCShipsData = new Dictionary<string, NPCShip>();
        foreach (var info in ShipTable.Instance.GetDictionary().Values)
        {
            if (!info.IsPlayerShip)
            {
                var ship = new NPCShip(info);
                _playerData.NPCShipsData.Add(info.ID,ship);
            }
        }
    }

    public override PlayerData GetData()
    {
        return _playerData;
    }
}
