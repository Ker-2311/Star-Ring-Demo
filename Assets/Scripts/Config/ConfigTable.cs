using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Reflection;
using ExcelDataReader;
using Exterior;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// 配置数据类型基类
/// </summary>
public class BaseInfo
{
    [LabelText("ID"), ReadOnly]
    public string ID;
}
/// <summary>
/// 配置表基类
/// </summary>
/// <typeparam name="TData">配置数据类型</typeparam>
/// <typeparam name="T">单例对象</typeparam>
public class ConfigTable<TData, T> : Singleton<T>
    where TData : BaseInfo,new()
    where T : Singleton<T>,new()
{
    public string TableName;
    private string _tablePath;
    //第二行的属性名
    private string _propertyName;
    //保存string格式数据
    private List<List<string>> _strings = new List<List<string>>();
    //ID字典
    private Dictionary<string, TData> _cache = new Dictionary<string, TData>();

    /// <summary>
    /// 读表
    /// </summary>
    /// <param name="tablePath"></param>
    protected void Load(string tablePath)
    {
        _cache = LoadTable(tablePath);
    }

    /// <summary>
    /// 重新加载配置表，仅编辑器模式下可用
    /// </summary>
    /// <returns></returns>
    public void ReLoad()
    {
#if UNITY_EDITOR
        if(_tablePath != null)
            _cache = LoadTable(_tablePath);
#endif
    }

    private Dictionary<string, TData> LoadTable(string tablePath)
    {
        TableName = Path.GetFileNameWithoutExtension(tablePath);
        if (_tablePath == null) _tablePath = tablePath;
        var table = ResMgr.Instance.GetResource<TextAsset>(tablePath);
        var tableStream = new MemoryStream(table.bytes);
        Dictionary<string, TData> tableDic = new Dictionary<string, TData>();

        using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("utf-8")))//Encoding.GetEncoding("gb2312")
        {
            _strings.Clear();
            //第一行注释
            _strings.Add(reader.ReadLine().Split(',').ToList());
            //获取第二行数据类型字段名
            var fileNameStr = reader.ReadLine();
            var fileNameArray = fileNameStr.Split(',');

            _propertyName = fileNameStr;
            _strings.Add(fileNameArray.ToList());
            List<FieldInfo> allFieldsInfo = new List<FieldInfo>();


            //反射获取数据类型字段
            foreach (var fileName in fileNameArray)
            {
                if (typeof(TData).GetField(fileName) == null) { allFieldsInfo.Add(null); continue; }
                allFieldsInfo.Add(typeof(TData).GetField(fileName));
            }
            //循环读取
            while (!reader.EndOfStream)
            {
                var dbLine = reader.ReadLine().Split(',');
                _strings.Add(dbLine.ToList());
                //当读入的文件第一列有Stop停止读取
                if (dbLine[0] == "Stop")
                {
                    break;
                }
                var roleData = new TData();

                for (int i = 0; i < allFieldsInfo.Count; i++)
                {
                    //如果此格子为空或者字段不存在
                    if (dbLine[i] == "" || allFieldsInfo[i] == null)
                    {
                        continue;
                    }
                    Paraser(allFieldsInfo[i], dbLine[i], roleData);
                }
                if (tableDic.ContainsKey(roleData.ID)) { Debug.LogError("存在相同ID"); }

                tableDic[roleData.ID] = roleData;
            }
        }
        return tableDic;
    }

    /// <summary>
    /// 解析器
    /// </summary>
    /// <param name="fieldsInfo"></param>
    /// <param name="dbLine"></param>
    /// <param name="roleData"></param>
    /// <param name="i"></param>
    protected virtual void Paraser(FieldInfo fieldsInfo, string db, TData roleData)
    {
        if (fieldsInfo.FieldType == typeof(string)) { fieldsInfo.SetValue(roleData, db); }
        else if (fieldsInfo.FieldType == typeof(int)) { fieldsInfo.SetValue(roleData, int.Parse(db)); }
        else if (fieldsInfo.FieldType == typeof(float)) { fieldsInfo.SetValue(roleData, float.Parse(db)); }
        else if (fieldsInfo.FieldType == typeof(bool))
        {
            if (db == "True" || db == "true")
            {
                fieldsInfo.SetValue(roleData, true);
            }
            else if (db == "False" || db == "false")
            {
                fieldsInfo.SetValue(roleData, false);
            }
            else if (int.Parse(db) >= 1)
            {
                fieldsInfo.SetValue(roleData, true);
            }
            else
            {
                fieldsInfo.SetValue(roleData, false);
            }
        }
        else if (fieldsInfo.FieldType == typeof(Dictionary<string, int>))
        {
            var dic = new Dictionary<string, int>();
            var element = db.Split(';');
            foreach (var pair in element)
            {
                var keyValue = pair.Split(':');
                dic.Add(keyValue[0], int.Parse(keyValue[1]));
            }
            fieldsInfo.SetValue(roleData, dic);
        }
        else if (fieldsInfo.FieldType == typeof(List<string>))
        {
            var list = new List<string>();
            var element = db.Split(';');
            foreach (var elem in element)
            {
                list.Add(elem);
            }
            fieldsInfo.SetValue(roleData, list);
        }
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="RoleID">字符类型ID</param>
    /// <returns></returns>
    public TData this[string RoleID]
    {
        get
        {
            TData db;
            _cache.TryGetValue(RoleID, out db);
            return db;
        }
    }

    /// <summary>
    /// 获取配置表的字典
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, TData> GetDictionary()
    {
        return _cache;
    }

    /// <summary>
    /// 获取配置表的数据(string格式)
    /// </summary>
    /// <returns></returns>
    public string[,] GetStrings()
    {
        if (_strings.Count == 0) return null;
        var strings = new string[_strings.Count, _strings[0].Count];
        int i = 0, j = 0;
        foreach (var row in _strings)
        {
            foreach (var column in row)
            {
                try
                {
                    strings[i, j] = column;
                    j++;
                }
                catch
                {
                    Debug.Log("");
                }
            }
            i++;
            j = 0;
        }
        return strings;
    }

    /// <summary>
    /// 获取配置表所有变量名，方便反射读取变量
    /// </summary>
    /// <returns></returns>
    public string[] GetDictionaryPropertyNames()
    {
        return _propertyName.Split(',');
    }
}

