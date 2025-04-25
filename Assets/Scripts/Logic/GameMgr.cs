using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private GameObject _engineRoot;
    public bool InitFinish = false;
    /// <summary>
    /// ��������Ϸʱ��ʼ��
    /// </summary>
    public void SetupInit()
    {
        //��ʼ������������
        if (_engineRoot == null)
        {
            _engineRoot = new GameObject("GameEngine");
            GameObject.DontDestroyOnLoad(_engineRoot);

            _engineRoot.AddComponent<GameEngine>();
        }

        //��ʼ��UIsystem(����UI��ʼ����������ʼ�����ڿ�ʼ����Ϸʱִ��)
        UIManager.Instance.Init();
        KeyboardEventBinding.Instance.Init();
        MouseEventBlinding.Instance.Init();
        LoadCursorControl("Prefabs/Cursor/CursorControl");
    }

    /// <summary>
    /// �ڽ���Basic����ʱ��ʼ��
    /// </summary>
    public IEnumerator LoadingInit()
    {
        //����Ӧ�ڶ�ȡ�浵��ʼ��Ϸʱִ�г�ʼ��
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
        //Ԥ��������Panel
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
        ////���Դ���
        //StationMgr.Instance.BuildStation("5");
    }

    /// <summary>
    /// Ԥ����Panel
    /// </summary>
    /// <param name="path"></param>
    public void PreLoadPanel(string path)
    {
        PanelMgr.Instance.PreLoadPanel(path);
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="path"></param>
    public void LoadCursorControl(string path)
    {
        var cursorControl = ResMgr.Instance.GetInstance(path);
        GameObject.DontDestroyOnLoad(cursorControl);
    }
}
