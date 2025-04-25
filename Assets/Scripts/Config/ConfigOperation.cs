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
/// 对配置文件进行修改
/// </summary>
public class ConfigOperation
{
    private static string _tablePath = Application.dataPath + "/../Config/";
#if UNITY_EDITOR
    /// <summary>
    /// 修改配置表
    /// </summary>
    /// <param name="tableName">配置表名，如WeaponTable</param>
    public static void AlterTable(string tableName, string[,] dataSet)
    {
        //读取xlsx表
        var filePath = _tablePath + tableName + ".xlsx";
        //开始写入
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
            //创建数据资源文件
            //泛型是继承自ScriptableObject的类
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
                //通过编辑器API，创建一个数据资源文件，第二个参数为资源文件在Assets目录下的路径
                AssetDatabase.CreateAsset(asset, path);
            }
            EditorUtility.SetDirty(asset);
            //保存创建的资源
            AssetDatabase.SaveAssets();
        }
    }

    public static WeaponConfigObject GetWeaponConfigObject(string name)
    {
        return ResMgr.Instance.GetResource<WeaponConfigObject>("Config/Object/Weapon/" + name);
    }

    /// <summary>
    /// 解析属性，转化为字符串
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
    /// 查找配置表字符串数组中的ID所在行号
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
    /// 应用配置表修改
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

            Debug.Log("配置文件复制完毕");
        }
    }

    private static void WriteTable()
    {

    }

    /// <summary>
    /// 读Excel
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static System.Data.DataSet ReadTable(string tableName)
    {
        //读取xlsx表
        var filePath = _tablePath + tableName + ".xlsx";
        if (!File.Exists(filePath)) return null;
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var excelReader = ExcelReaderFactory.CreateReader(fileStream);
        var dataSet = excelReader.AsDataSet();
        //如果没有表则返回
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
