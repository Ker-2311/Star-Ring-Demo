using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShipMgr : Singleton<NPCShipMgr>
{
    private Dictionary<string,NPCShip> NPCShips
    {
        get { return DataMgr.Instance.PlayerData.NPCShipsData; }
        set { DataMgr.Instance.PlayerData.NPCShipsData = value; }
    }

    public NPCShip GetShip(string name)
    {
        return NPCShips[ShipTable.Instance.GetFromName(name).ID];
    }
}
