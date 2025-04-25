using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 建筑类型
/// </summary>
public enum BuildingType
{
    Sources,
    Function
}

public class BuildingInfo:BaseInfo
{
    public string Name;
    public string Descript;
    public string Cost;
    public Dictionary<string, int> BuildingSourcesCost;
    public int BuildingTime;
    public int MaxRank;
    public int StationMaxBuilded;
    public int SpaceMaxBuilded;
    public BuildingType BuildingType;
    public string BuildingEffectType;
    public Dictionary<string, int> MouthSourcesCost;
    public Dictionary<string, int> MouthSourcesOutput;
    public Dictionary<string, int> MouthMaterialOutput;
}
public class BuildingTable : ConfigTable<BuildingInfo,BuildingTable>
{
    public BuildingTable()
    {
        Load("Config/Table/BuildingTable.csv");
    }

    protected override void Paraser(FieldInfo allFieldsInfo, string db, BuildingInfo roleData)
    {
        base.Paraser(allFieldsInfo, db, roleData);
        if (db == "")
        {
            return;
        }
        if (allFieldsInfo.FieldType == typeof(BuildingType))
        {
            switch (db)
            {
                case "资源类": allFieldsInfo.SetValue(roleData, BuildingType.Sources); break;
                case "功能类": allFieldsInfo.SetValue(roleData, BuildingType.Function); break;
            }
        }
    }
}
