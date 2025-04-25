using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechInfo : BaseInfo
{
    public string Name;
    public string Type;
    public string Descript;
    public string FrontID;
    public int PointCost;
    public int CoinCost;
}

public class TechTable : ConfigTable<TechInfo, TechTable>
{
    public TechTable()
    {
        Load("Config/Table/TechTable.csv");
    }
}
