using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcesInfo:BaseInfo
{
    public string Name;
    public string Description;

}

public class SourcesTable : ConfigTable<SourcesInfo, SourcesTable>
{
    public SourcesTable()
    {
        Load("Config/Table/SourcesTable.csv");
    }
}
