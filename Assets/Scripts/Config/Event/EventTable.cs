using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo : BaseInfo
{
    public string Name;
    public string EventType;
    public string Description;
    public string TriggerCondition;
    public string Option1;
    public string Option2;
    public string Option3;
    public string Option4;
    public string Option5;
    public string Option6;
    public string Option1Description;
    public string Option2Description;
    public string Option3Description;
    public string Option4Description;
    public string Option5Description;
    public string Option6Description;
}

public class EventTable : ConfigTable<EventInfo, EventTable>
{
    public EventTable()
    {
        Load("Config/Table/EventTable.csv");
    }
}
