using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OdinEditor
{
    public class CombatEditorWindow : OdinMenuEditorWindow
    {

        [MenuItem("�༭��/ս���༭��")]
        private static void Open()
        {
            var window = GetWindow<CombatEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 400);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            //tree.Config.DrawSearchToolbar = true;

            tree.Add("װ���༭��", null);
            tree.Add("װ���༭��/�����༭��", new WeaponEditor(WeaponTable.Instance));
            tree.Add("NPC�����༭��", new ShipEditor(ShipTable.Instance));
            return tree;
        }

        //protected override void OnBeginDrawEditors()
        //{
        //    var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        //    SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight,50);
        //    {
        //        if (SirenixEditorGUI.ToolbarButton(new GUIContent("ȫ������")))
        //        {
        //            _weaponEditor.Save();
        //            TableOperation.ApplyAlter();
        //        }
        //    }
        //    SirenixEditorGUI.EndHorizontalToolbar();
        //}
    }
}

