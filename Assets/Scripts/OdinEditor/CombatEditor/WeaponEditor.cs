using ECS.Combat;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinEditor
{
    [Serializable]
    /// <summary>
    /// ��Ʒ�˵�����
    /// </summary>
    public class WeaponTableItem:ConfigItem<WeaponInfo>
    {
        [PreviewField]
        [VerticalGroup("ͼ��"), HideLabel, ReadOnly, TableColumnWidth(55, false)]
        public Texture Icon;

        [VerticalGroup("����"), HideLabel, ReadOnly]
        public string Name;

        [VerticalGroup("����"), TextArea, HideLabel, ReadOnly]
        public string Description;


        [VerticalGroup("�༭��ť")]
        [Button(Name = "�༭����")]
        private void OpenWeaponPropertyEditorWindow()
        {
            ScriptableObject.CreateInstance<WeaponEditorWindow>().Open(this);
        }
    }


    public class WeaponEditor : ConfigEditor<WeaponInfo,WeaponTable, WeaponTableItem>
    {

        public WeaponEditor(WeaponTable table):base(table)
        {
            
        }
        protected override void Init()
        {
            base.Init();
            var weaponTable = WeaponTable.Instance.GetDictionary();
            TableItems = new List<WeaponTableItem>();
            foreach (var info in weaponTable.Values)
            {
                var item = new WeaponTableItem();
                item.info = info;
                item.Name = info.Name;
                item.Description = info.Description;
                TableItems.Add(item);
            }
        }

        public override void Save()
        {
            base.Save();
            ConfigOperation.GenerateWeaponConfigObject();
        }

    }

    public class WeaponEditorWindow : ConfigEditorWindow<WeaponInfo,WeaponTableItem>
    {
        public override void Open(WeaponTableItem tableItem)
        {
            base.Open(tableItem);
            WeaponInfo = tableItem.info;
        }

        protected override void SaveItem()
        {
            base.SaveItem();
            _tableItem.Name = _tableItem.info.Name;
            _tableItem.Description = _tableItem.info.Description;
            _tableItem.info = WeaponInfo;
        }

        [LabelText("��������"), ShowInInspector]
        public WeaponInfo WeaponInfo;
    }
}
