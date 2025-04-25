using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinEditor
{
    [Serializable]
    /// <summary>
    /// 物品菜单表项
    /// </summary>
    public class ShipTableItem : ConfigItem<ShipInfo>
    {
        [PreviewField]
        [VerticalGroup("图标"), HideLabel, ReadOnly, TableColumnWidth(55, false)]
        public Texture Icon;

        [VerticalGroup("名字"), HideLabel, ReadOnly]
        public string Name;

        [VerticalGroup("描述"), TextArea, HideLabel, ReadOnly]
        public string Description;


        [VerticalGroup("编辑按钮")]
        [Button(Name = "编辑属性")]
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

        [LabelText("基本属性"),ShowInInspector]
        public ShipInfo ShipInfo;
    }


}
