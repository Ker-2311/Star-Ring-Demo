using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Exterior;

public static class EasyEditor
{

    [MenuItem("工具栏/应用配置更改")]
    public static void ConfigToResource()
    {
        ConfigOperation.ApplyAlter();
        ConfigOperation.GenerateWeaponConfigObject();
        AssetDatabase.Refresh();

        Debug.Log("配置文件复制完毕");
    }

    [MenuItem("工具栏/打开配置文件夹")]
    public static void Test()
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/../Config/");
    }

    [MenuItem("工具栏/切换到游戏开始场景")]
    public static void GoToSetup()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Setup.unity");
    }

    [MenuItem("工具栏/切换到UI测试场景")]
    public static void GoToUIEditor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/UIEditor.unity");
    }

    [MenuItem("工具栏/切换到主场景")]
    public static void GoToBasic()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Basic.unity");
    }

    [MenuItem("工具栏/切换到测试场景")]
    public static void GoToTest()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Test.unity");
    }

    [MenuItem("工具栏/切换到游戏开始界面场景")]
    public static void GoToStart()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Start.unity");
    }

    [MenuItem("工具栏/切换到战斗测试场景")]
    public static void GoToFightTest()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/FightTest.unity");
    }
}
