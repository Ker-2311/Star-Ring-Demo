using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataDirector
{
    public void MakePlayerData(PlayerDataBuilder builder)
    {
        builder.InitBuildingLockData();
        builder.InitForceData();
        builder.InitInventoryData();
        builder.InitMoneyData();
        builder.InitScienceData();
        builder.InitSourcesData();
        builder.InitStarData();
        builder.InitStationData();
        builder.InitEquipmentData();
        builder.InitTimeData();
        builder.InitPlayerShipsData();
        builder.InitActivePlayerShip();
        builder.InitNPCShipsData();
    }

}
