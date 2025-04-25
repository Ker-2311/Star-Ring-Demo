using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugCommand
{
    public static void Print(string text)
    {
        Debug.Log(text);
    }

    public static void UnlockScience(string id)
    {
        ScienceAndTechMgr.Instance.UnConditionUnlockScience(id);
    }

    public static void UnlockAllScience()
    {
        ScienceAndTechMgr.Instance.UnConditionUnlockAllScience();
    }

    public static void TriggerEvent(string id)
    {
        GameEventMgr.Instance.EventTrigger(id);
    }
}
