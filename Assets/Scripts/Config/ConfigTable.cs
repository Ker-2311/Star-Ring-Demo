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
/// �����������ͻ���
/// </summary>
public class BaseInfo
{
    [LabelText("ID"), ReadOnly]
    public string ID;
}
/// <summary>
/// ���ñ����
/// </summary>
/// <typeparam name="TData">������������</typeparam>
/// <typeparam name="T">��������</typeparam>
public class ConfigTable<TData, T> : Singleton<T>
    where TData : BaseInfo,new()
    where T : Singleton<T>,new()
{
    public string TableName;
    private string _tablePath;
    //�ڶ��е�������
    private string _propertyName;
    //����string��ʽ����
    private List<List<string>> _strings = new List<List<string>>();
    //ID�ֵ�
    private Dictionary<string, TData> _cache = new Dictionary<string, TData>();

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="tablePath"></param>
    protected void Load(string tablePath)
    {
        _cache = LoadTable(tablePath);
    }

    /// <summary>
    /// ���¼������ñ����༭��ģʽ�¿���
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
            //��һ��ע��
            _strings.Add(reader.ReadLine().Split(',').ToList());
            //��ȡ�ڶ������������ֶ���
            var fileNameStr = reader.ReadLine();
            var fileNameArray = fileNameStr.Split(',');

            _propertyName = fileNameStr;
            _strings.Add(fileNameArray.ToList());
            List<FieldInfo> allFieldsInfo = new List<FieldInfo>();


            //�����ȡ���������ֶ�
            foreach (var fileName in fileNameArray)
            {
                if (typeof(TData).GetField(fileName) == null) { allFieldsInfo.Add(null); continue; }
                allFieldsInfo.Add(typeof(TData).GetField(fileName));
            }
            //ѭ����ȡ
            while (!reader.EndOfStream)
            {
                var dbLine = reader.ReadLine().Split(',');
                _strings.Add(dbLine.ToList());
                //��������ļ���һ����Stopֹͣ��ȡ
                if (dbLine[0] == "Stop")
                {
                    break;
                }
                var roleData = new TData();

                for (int i = 0; i < allFieldsInfo.Count; i++)
                {
                    //����˸���Ϊ�ջ����ֶβ�����
                    if (dbLine[i] == "" || allFieldsInfo[i] == null)
                    {
                        continue;
                    }
                    Paraser(allFieldsInfo[i], dbLine[i], roleData);
                }
                if (tableDic.ContainsKey(roleData.ID)) { Debug.LogError("������ͬID"); }

                tableDic[roleData.ID] = roleData;
            }
        }
        return tableDic;
    }

    /// <summary>
    /// ������
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
    /// ������
    /// </summary>
    /// <param name="RoleID">�ַ�����ID</param>
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
    /// ��ȡ���ñ���ֵ�
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, TData> GetDictionary()
    {
        return _cache;
    }

    /// <summary>
    /// ��ȡ���ñ������(string��ʽ)
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
    /// ��ȡ���ñ����б����������㷴���ȡ����
    /// </summary>
    /// <returns></returns>
    public string[] GetDictionaryPropertyNames()
    {
        return _propertyName.Split(',');
    }
}

