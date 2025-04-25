using ExcelDataReader;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using OfficeOpenXml;
using System.Reflection;
using Exterior;
using UnityEditor;
using System;

/// <summary>
/// �������ļ������޸�
/// </summary>
public class ConfigOperation
{
    private static string _tablePath = Application.dataPath + "/../Config/";
#if UNITY_EDITOR
    /// <summary>
    /// �޸����ñ�
    /// </summary>
    /// <param name="tableName">���ñ�������WeaponTable</param>
    public static void AlterTable(string tableName, string[,] dataSet)
    {
        //��ȡxlsx��
        var filePath = _tablePath + tableName + ".xlsx";
        //��ʼд��
        var newFile = new FileStream(filePath, FileMode.Truncate, FileAccess.Write);
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet0");

            for (int i = 0; i < dataSet.GetLength(0); i++)
            {
                for (int j = 0; j < dataSet.GetLength(1); j++)
                {
                    worksheet.Cells[i + 1, j + 1].Value = dataSet[i,j];
                }
            }
            package.Save();
        }
        newFile.Close();
        ApplyAlter(tableName);
    }

    public static void GenerateWeaponConfigObject()
    {
        foreach (var info in WeaponTable.Instance.GetDictionary().Values)
        {
            //����������Դ�ļ�
            //�����Ǽ̳���ScriptableObject����
            WeaponConfigObject asset = ScriptableObject.CreateInstance<WeaponConfigObject>();
            string path = "Assets/Resources/Config/Object/Weapon/" + info.Name + ".asset";

            if (File.Exists(path))
            {
                asset = AssetDatabase.LoadAssetAtPath<WeaponConfigObject>(path);
                asset.Info = info;
                asset.BulletPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Bullet/" + info.Name);
                asset.WeaponPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Weapon/" + info.Name);
            }
            else
            {
                asset.Info = info;
                asset.BulletPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Bullet/" + info.Name);
                asset.WeaponPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/Fight/Weapon/" + info.Name);
                //ͨ���༭��API������һ��������Դ�ļ����ڶ�������Ϊ��Դ�ļ���AssetsĿ¼�µ�·��
                AssetDatabase.CreateAsset(asset, path);
            }
            EditorUtility.SetDirty(asset);
            //���洴������Դ
            AssetDatabase.SaveAssets();
        }
    }

    public static WeaponConfigObject GetWeaponConfigObject(string name)
    {
        return ResMgr.Instance.GetResource<WeaponConfigObject>("Config/Object/Weapon/" + name);
    }

    /// <summary>
    /// �������ԣ�ת��Ϊ�ַ���
    /// </summary>
    /// <param name="parmeter"></param>
    /// <param name="fieldsInfo"></param>
    /// <returns></returns>
    public static string Parser(object parmeter,FieldInfo fieldsInfo)
    {
        if (parmeter == null) return null;
        string str = "";
        if (fieldsInfo.FieldType == typeof(List<string>)) 
        {
            var field = parmeter as List<string>;
            foreach (var fieldStr in field)
            {
                str += (fieldStr + ";");
            }
        }
        else if (fieldsInfo.FieldType == typeof(Dictionary<string, int>))
        {
            var field = parmeter as Dictionary<string, int>;
            foreach (var fieldPair in field)
            {
                str += (fieldPair.Key + ":" + fieldPair.Value + ";");
            }
        }
        else if (fieldsInfo.FieldType == typeof(BulletFlightType))
        {
            str = Enum.GetName(typeof(BulletFlightType),(BulletFlightType)parmeter);

        }
        else
        {
            str = parmeter.ToString();
        }
        str = str?.TrimEnd(';');
        return str;
    }

    /// <summary>
    /// �������ñ��ַ��������е�ID�����к�
    /// </summary>
    /// <param name="dataSet"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static int ArrayIDFind(string[,] dataSet,string id)
    {
        int index = -1;
        for (int i =0; i< dataSet.Length;i++)
        {
            if(dataSet[i,0] == id)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    /// <summary>
    /// Ӧ�����ñ��޸�
    /// </summary>
    public static void ApplyAlter()
    {
        var srcpath = Application.dataPath + "/../Config/";
        var detpath = Application.dataPath + "/Resources/Config/Table/";

        Directory.Delete(detpath, true);
        Directory.CreateDirectory(detpath);

        foreach (var filepath in Directory.GetFiles(srcpath))
        {
            var filename = Path.GetFileName(filepath);
            if (filename.Contains(".xlsx"))
            {
                FileTools.ConvertCSV(filepath, detpath, filename.Replace(".xlsx", ".csv") + ".bytes");
                continue;
            }
            //File.Copy(filepath, detpath + filename + ".bytes", true);
        }
    }

    public static void ApplyAlter(string tableName)
    {
        var srcpath = Application.dataPath + "/../Config/";
        var detpath = Application.dataPath + "/Resources/Config/Table/";
        var filepath = srcpath + tableName + ".xlsx";
        var detfilepath = detpath + tableName + ".csv.bytes";

        File.Delete(detfilepath);

        if (File.Exists(filepath))
        {
            FileTools.ConvertCSV(filepath, detpath, Path.GetFileNameWithoutExtension(filepath) + ".csv.bytes");
            AssetDatabase.Refresh();

            Debug.Log("�����ļ��������");
        }
    }

    private static void WriteTable()
    {

    }

    /// <summary>
    /// ��Excel
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static System.Data.DataSet ReadTable(string tableName)
    {
        //��ȡxlsx��
        var filePath = _tablePath + tableName + ".xlsx";
        if (!File.Exists(filePath)) return null;
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var excelReader = ExcelReaderFactory.CreateReader(fileStream);
        var dataSet = excelReader.AsDataSet();
        //���û�б��򷵻�
        if (dataSet.Tables.Count < 1) return null;
        if (dataSet.Tables[0].Rows.Count < 1) return null;

        fileStream.Close();
        return dataSet;
    }

    private static string[,] DataSetAsStrings(System.Data.DataSet dataSet)
    {
        string[,] strings = new string[dataSet.Tables[0].Rows.Count,dataSet.Tables[0].Columns.Count];
        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < dataSet.Tables[0].Columns.Count; j++)
            {
                strings[i,j] = dataSet.Tables[0].Rows[i][j].ToString();
            }
        }
        return strings;
    }

#endif
}
