using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShip:IShipData
{
    public ShipInfo Info { get; set; }

    public ShipAttribute ShipAttribute { get; set; }

    public NPCShip(ShipInfo info)
    {
        Info = info;
        ShipAttribute = new ShipAttribute(info);
    }
}
