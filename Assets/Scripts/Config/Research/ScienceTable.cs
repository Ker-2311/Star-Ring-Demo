using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceInfo : BaseInfo
{
    public string Name;
    public string Type;
    public string Descript;
    public string FrontID;
    public int PointCost;
    public int CoinCost;
}

public class ScienceTable : ConfigTable<ScienceInfo, ScienceTable>
{
    public ScienceTable()
    {
        Load("Config/Table/ScienceTable.csv");
    }
}
