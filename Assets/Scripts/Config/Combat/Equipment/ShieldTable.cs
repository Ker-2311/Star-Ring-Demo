using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldType : BaseInfo
{

}

public class ShieldTable : ConfigTable<ShieldType, ShieldTable>
{
    public ShieldTable()
    {
        Load("Config/Table/Equipment/ShieldTable.csv");
    }
}