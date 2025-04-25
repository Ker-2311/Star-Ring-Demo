using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MaterialInfo:BaseInfo
{
    public string Name;
    public int Price;
    public string Description;
    public int EnergyCost;
    public int MineralCost;
    public int SupplyCost;
    public string IconPath;
    public int Rank;
    public int UnitLoad;
    public Dictionary<string, int> SpecialCost;
}

public class MaterialTable : ConfigTable<MaterialInfo,MaterialTable>
{
    public MaterialTable()
    {
        Load("Config/Table/MaterialTable.csv");
    }

}
