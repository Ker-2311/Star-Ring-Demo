using Exterior;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardEventBinding;

/// <summary>
/// 主菜单，管理各系统的进入按钮，不进入UI堆栈
/// </summary>
public class MainPanel : MonoBehaviour
{
    private GameObject[] _cmdPrefabs;
    private GameObject _resourcesFormat;
    private GameObject _cmdFormat;
    private GameObject _gameTime;
    private GameObject _info;
    private GameObject _buttonGroup;
    public void Awake()
    {
        _info = transform.Find("MainPanelInfo").gameObject;
        _resourcesFormat = transform.Find("MainPanelInfo/ResourcesFormat").gameObject;
        _cmdFormat = transform.Find("MainPanelInfo/CmdFormat").gameObject;
        _gameTime = transform.Find("MainPanelInfo/GameTime").gameObject;
        _cmdPrefabs = ResMgr.Instance.GetAllResources<GameObject>("Prefabs/UI/Basic/Main/Cmd");
        _buttonGroup = transform.Find("ButtonGroup").gameObject;

        ButtonAddListener(_buttonGroup.transform);
        //事件面板
        var cmdAnimator = _cmdFormat.GetComponent<Animator>();
        var cmdShrinkButton = _cmdFormat.transform.Find("ShrinkButton").GetComponent<Toggle>();
        var cmdNew = cmdShrinkButton.transform.Find("New");
        cmdShrinkButton.onValueChanged.AddListener((bool IsOn) =>
        {
            if (IsOn)
            {
                cmdAnimator.SetBool("IsShrink", true);
            }
            else
            {
                cmdAnimator.SetBool("IsShrink", false);
            }
        });

        //资源面板
        var resAnimator = _resourcesFormat.GetComponent<Animator>();
        var resShrinkButton = _resourcesFormat.transform.Find("ShrinkButton").GetComponent<Toggle>();
        resShrinkButton.onValueChanged.AddListener((bool IsOn) =>
        {
            if (IsOn)
            {
                resAnimator.SetBool("IsShrink", true);
            }
            else
            {
                resAnimator.SetBool("IsShrink", false);
            }
        });

        //Esc退出UI
        KeyboardEventBinding.Instance.BindKeyboardEvent(KeyboardStatus.MainPanel, KeyCode.Escape, EscapeEvent);
    }

    private void OnEnable()
    {
        KeyboardEventBinding.Instance.ChangeStatus(KeyboardEventBinding.KeyboardStatus.MainPanel);
        MouseEventBlinding.Instance.ChangeStatus(MouseEventBlinding.MouseEventStatus.MainPanel);
    }

    private void Update()
    {
        //更新GameTime显示
        var gameTime = GameTimeMgr.Instance.GetGameTime();
        _gameTime.transform.Find("Time").GetComponent<Text>().text = gameTime.Years.ToString() + "." + gameTime.Mouths.ToString() + "."
            + gameTime.Days.ToString();
        //Event判定
        var gameEvent = GameEventMgr.Instance.ShowEvent();
        if (gameEvent != null)
        {
            GameObject cmdPrefab;
            if (gameEvent.eventInfo.EventType == "危机事件")
            {
                cmdPrefab = _cmdPrefabs[0];
            }
            else
            {
                cmdPrefab = _cmdPrefabs[1];
            }
            var cmd = ResMgr.Instance.GetInstance(cmdPrefab, _cmdFormat.transform.Find("Scroll View/Viewport/Content"));
            cmd.GetComponent<Cmd>().Init(gameEvent);
            
        }
    }

    /// <summary>
    /// 给ButtonGroup按钮添加点击事件
    /// </summary>
    /// <param name="buttonGroup"></param>
    private void ButtonAddListener(Transform buttonGroup)
    {
        var InventoryButton = buttonGroup.Find("InventoryButton").GetComponent<Toggle>();
        var mapButton = buttonGroup.Find("MapButton").GetComponent<Toggle>();
        var SettingButton = buttonGroup.Find("SettingButton").GetComponent<Toggle>();
        var ResearchButton = buttonGroup.Find("ResearchButton").GetComponent<Toggle>();
        var ConfigButton = buttonGroup.Find("ConfigButton").GetComponent<Toggle>();
        var InfluenceButton = buttonGroup.Find("BuildingButton").GetComponent<Toggle>();
        var timeStartButton = _gameTime.transform.Find("StartButton").GetComponent<Toggle>();
        var timePauseButton = _gameTime.transform.Find("PauseButton").GetComponent<Toggle>();
        var timeAccelerateButton = _gameTime.transform.Find("AccelerateButton").GetComponent<Toggle>();
        //添加各个按钮事件
        InventoryButton.onValueChanged.AddListener((bool IsOn) => OnToggleValueChange(IsOn, "Prefabs/UI/Basic/Inventory/InventoryPanel"));
        mapButton.onValueChanged.AddListener((bool IsOn) =>
        {
            if (IsOn)
            {
                _info.SetActive(true);
                //StarSystemMgr.instance.ExitStarSystem();
                PanelMgr.Instance.PopAllPanel();
            }
            else
            {
                _info.SetActive(false);
            }
        });
        ResearchButton.onValueChanged.AddListener((bool IsOn) => OnToggleValueChange(IsOn, "Prefabs/UI/Basic/Research/ResearchPanel"));
        ConfigButton.onValueChanged.AddListener((bool IsOn) => OnToggleValueChange(IsOn, "Prefabs/UI/Basic/Config/ConfigPanel"));
        InfluenceButton.onValueChanged.AddListener((bool IsOn) => OnToggleValueChange(IsOn, "Prefabs/UI/Basic/Influence/InfluencePanel"));
        SettingButton.onValueChanged.AddListener((bool IsOn) => { if (IsOn) GameExit(); });
        timeStartButton.onValueChanged.AddListener((bool IsOn) => { if (IsOn) GameTimeMgr.Instance.StartTime(); });
        timePauseButton.onValueChanged.AddListener((bool IsOn) => { if (IsOn) GameTimeMgr.Instance.PauseTime(); });
        //timeAccelerateButton.onValueChanged.AddListener((bool IsOn) => { if (IsOn) GameTimeMgr.Instance.AccelerateTime(2f); });
    }
    /// <summary>
    /// Esc按键触发事件
    /// </summary>
    private void EscapeEvent()
    {
        PanelMgr.Instance.Pop();
        if (PanelMgr.Instance.Peek() == null)
        {
            _buttonGroup.transform.Find("MapButton").GetComponent<Toggle>().isOn = true;
        }
    }

    private void OnToggleValueChange(bool IsOn, string path)
    {
        if (IsOn)
        {
            PanelMgr.Instance.PopAllPanel();
            PanelMgr.Instance.Push(path);
        }
    }

    /// <summary>
    /// 退出游戏 
    /// </summary>
    private void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }

}
