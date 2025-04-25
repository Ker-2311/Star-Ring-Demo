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

    [MenuItem("������/Ӧ�����ø���")]
    public static void ConfigToResource()
    {
        ConfigOperation.ApplyAlter();
        ConfigOperation.GenerateWeaponConfigObject();
        AssetDatabase.Refresh();

        Debug.Log("�����ļ��������");
    }

    [MenuItem("������/�������ļ���")]
    public static void Test()
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/../Config/");
    }

    [MenuItem("������/�л�����Ϸ��ʼ����")]
    public static void GoToSetup()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Setup.unity");
    }

    [MenuItem("������/�л���UI���Գ���")]
    public static void GoToUIEditor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/UIEditor.unity");
    }

    [MenuItem("������/�л���������")]
    public static void GoToBasic()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Basic.unity");
    }

    [MenuItem("������/�л������Գ���")]
    public static void GoToTest()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Test.unity");
    }

    [MenuItem("������/�л�����Ϸ��ʼ���泡��")]
    public static void GoToStart()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Start.unity");
    }

    [MenuItem("������/�л���ս�����Գ���")]
    public static void GoToFightTest()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/FightTest.unity");
    }
}
