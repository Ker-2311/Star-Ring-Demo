using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : BasePanel
{
    private string _name;
    public void Init(string name)
    {
        base.Init();
        _name = name;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PanelMgr.Instance.PopAllPanel();
                return;
            }
            StarSystemMgr.Instance.EnterStarSystem(_name);
            PanelMgr.Instance.PopAllPanel();
        }
    }
}
