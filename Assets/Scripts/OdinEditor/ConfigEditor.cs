using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinEditor
{
    /// <summary>
    /// ��Ʒ�˵�����
    /// </summary>
    public class ConfigItem<Info> where Info:BaseInfo
    {
        [HideInInspector]
        public Info info;
        protected virtual void OpenEditorWindow()
        {

        }
    }

    /// <summary>
    /// ���ñ�༭��
    /// </summary>
    public class ConfigEditor<Info,Table,Item>
        where Info:BaseInfo,new()
        where Table:ConfigTable<Info,Table>,new()
        where Item:ConfigItem<Info>
    {
        private Table _table;
        private static string _tableName;
        public ConfigEditor(Table table)
        {
            table.ReLoad();
            _table = table;
            _tableName = table.TableName;
            Init();
        }

        protected virtual void Init()
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        [Button("ȫ������")]
        public virtual void Save()
        {
            var infoPropertyNames = _table.GetDictionaryPropertyNames();
            var strings = new string[TableItems.Count + 2, infoPropertyNames.Length];
            var tableStrings = _table.GetStrings();
            for (int i = 0; (i < TableItems.Count + 2); i++)
            {
                for (int j = 0; j < infoPropertyNames.Length; j++)
                {
                    //д���һ����ڶ��еı�������ע��
                    if (i < 2)
                    {
                        strings[i, j] = tableStrings[i, j];
                    }
                    else
                    {
                        var type = TableItems[i - 2].info?.GetType().GetField(infoPropertyNames[j]);
                        if (type != null)
                        {
                            //�����ȡ�����ֶ�
                            var fieldValue = type.GetValue(TableItems[i - 2].info);
                            if (fieldValue == null) continue;
                            strings[i, j] = ConfigOperation.Parser(fieldValue, type);
                        }
                    }
                }
            }
            ConfigOperation.AlterTable(_tableName, strings);
        }

        [ShowInInspector]
        [LabelText("���ñ�"), TableList(AlwaysExpanded = true, ShowPaging = true, NumberOfItemsPerPage = 5)]
        public List<Item> TableItems;

    }

    /// <summary>
    /// ���ñ����༭����
    /// </summary>
    /// <typeparam name="Info"></typeparam>
    /// <typeparam name="Item"></typeparam>
    public class ConfigEditorWindow<Info, Item> : OdinEditorWindow
        where Info:BaseInfo,new()
        where Item:ConfigItem<Info>
    {
        protected Item _tableItem;
        public virtual void Open(Item tableItem)
        {
            if (tableItem.info == null)
            {
                tableItem.info = new Info() { ID = IDFactory.GenerateID()};
            }
            _tableItem = tableItem;
            var window = GetWindow<ConfigEditorWindow<Info, Item>>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(300, 600);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SaveItem();
        }

        protected virtual void SaveItem()
        {

        }

    }
}
