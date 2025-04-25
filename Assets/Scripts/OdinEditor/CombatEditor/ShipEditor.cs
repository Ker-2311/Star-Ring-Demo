using Sirenix.OdinInspector;
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
    public class ShipTableItem : ConfigItem<ShipInfo>
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
            ScriptableObject.CreateInstance<ShipEditorWindow>().Open(this);
        }
    }


    public class ShipEditor : ConfigEditor<ShipInfo, ShipTable, ShipTableItem>
    {

        public ShipEditor(ShipTable table) : base(table)
        {

        }
        protected override void Init()
        {
            base.Init();
            var tableDic = ShipTable.Instance.GetDictionary();
            TableItems = new List<ShipTableItem>();
            foreach (var info in tableDic.Values)
            {
                var item = new ShipTableItem();
                item.info = info;
                item.Name = info.Name;
                item.Description = info.Description;
                TableItems.Add(item);
            }
        }

    }

    public class ShipEditorWindow : ConfigEditorWindow<ShipInfo, ShipTableItem>
    {
        public override void Open(ShipTableItem tableItem)
        {
            base.Open(tableItem);
            ShipInfo = tableItem.info;
        }

        protected override void SaveItem()
        {
            base.SaveItem();
            _tableItem.Name = _tableItem.info.Name;
            _tableItem.Description = _tableItem.info.Description;
            _tableItem.info = ShipInfo;
        }

        [LabelText("��������"),ShowInInspector]
        public ShipInfo ShipInfo;
    }


}
