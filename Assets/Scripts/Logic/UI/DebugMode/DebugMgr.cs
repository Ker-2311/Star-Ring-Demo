using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMgr:Singleton<DebugMgr>
{
    public Dictionary<string, Action<object[]>> Commands = new Dictionary<string, Action<object[]>>();
    public void Init()
    {
        Type commands = typeof(DebugCommand);
        var methods = commands.GetMethods();
        var DebugPanelPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/UI/Basic/DebugModePanel");
        foreach (var method in methods)
        {
            if (method.ReturnType == typeof(void))
            {
                Commands.Add(method.Name, (object[] par) => method.Invoke(null, par));
            }
        }

        KeyboardEventBinding.Instance.BindKeyboardEvent(KeyboardEventBinding.KeyboardStatus.Global, KeyCode.F12,
            ()=>UIManager.Instance.AddUI(DebugPanelPrefab, UIManager.UILayer.Top));
    }
}
