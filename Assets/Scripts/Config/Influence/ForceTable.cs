using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInfo:BaseInfo
{
    public string Name;
    public string Description;
    public string IconPath;
}

public class ForceTable : ConfigTable<ForceInfo, ForceTable>
{
    public ForceTable()
    {
        Load("Config/Table/ForceTable.csv");
    }
}
