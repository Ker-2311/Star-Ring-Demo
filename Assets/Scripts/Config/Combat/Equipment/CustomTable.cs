using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomType : BaseInfo
{

}

public class CustomTable : ConfigTable<CustomType, CustomTable>
{
    public CustomTable()
    {
        Load("Config/Table/Equipment/CustomTable.csv");
    }
}
