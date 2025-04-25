using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private GameObject _engineRoot;
    public bool InitFinish = false;
    /// <summary>
    /// 在启动游戏时初始化
    /// </summary>
    public void SetupInit()
    {
        //初始化自驱动引擎
        if (_engineRoot == null)
        {
            _engineRoot = new GameObject("GameEngine");
            GameObject.DontDestroyOnLoad(_engineRoot);

            _engineRoot.AddComponent<GameEngine>();
        }

        //初始化UIsystem(除了UI初始化，其他初始化仅在开始新游戏时执行)
        UIManager.Instance.Init();
        KeyboardEventBinding.Instance.Init();
        MouseEventBlinding.Instance.Init();
        LoadCursorControl("Prefabs/Cursor/CursorControl");
    }

    /// <summary>
    /// 在进入Basic场景时初始化
    /// </summary>
    public IEnumerator LoadingInit()
    {
        //以下应在读取存档或开始游戏时执行初始化
        DataMgr.Instance.Init();
        yield return null;
        InventoryMgr.Instance.Init();
        yield return null;
        ScienceAndTechMgr.Instance.Init();
        yield return null;
        BuildingMgr.Instance.Init();
        yield return null;
        GameTimeMgr.Instance.Init();
        yield return null;
        StationMgr.Instance.Init();
        yield return null;
        ForceMgr.Instance.Init();
        yield return null;
        InfoPanelMgr.Instance.Init();
        yield return null;
        UnlockMgr.Instance.Init();
        yield return null;
        GameEventMgr.Instance.Init();
        yield return null;
        DebugMgr.Instance.Init();
        yield return null;
        //预加载所有Panel
        PreLoadPanel("Prefabs/UI/Basic/Inventory/InventoryPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Research/ResearchPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Config/ConfigPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Influence/InfluencePanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Influence/StationFunction/Ordnance/OrdnancePanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Influence/Building/BuildingListPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/Influence/Building/BuildingPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/Basic/EventPanel");
        yield return null;
        PreLoadPanel("Prefabs/UI/FightUI/FightPanel");
        InitFinish = true;
        ////测试代码
        //StationMgr.Instance.BuildStation("5");
    }

    /// <summary>
    /// 预加载Panel
    /// </summary>
    /// <param name="path"></param>
    public void PreLoadPanel(string path)
    {
        PanelMgr.Instance.PreLoadPanel(path);
    }

    /// <summary>
    /// 加载鼠标
    /// </summary>
    /// <param name="path"></param>
    public void LoadCursorControl(string path)
    {
        var cursorControl = ResMgr.Instance.GetInstance(path);
        GameObject.DontDestroyOnLoad(cursorControl);
    }
}
