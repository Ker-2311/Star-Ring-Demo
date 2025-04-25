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

        [MenuItem("±à¼­Æ÷/Õ½¶·±à¼­Æ÷")]
        private static void Open()
        {
            var window = GetWindow<CombatEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 400);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            //tree.Config.DrawSearchToolbar = true;

            tree.Add("×°±¸±à¼­Æ÷", null);
            tree.Add("×°±¸±à¼­Æ÷/ÎäÆ÷±à¼­Æ÷", new WeaponEditor(WeaponTable.Instance));
            tree.Add("NPC½¢´¬±à¼­Æ÷", new ShipEditor(ShipTable.Instance));
            return tree;
        }

        //protected override void OnBeginDrawEditors()
        //{
        //    var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        //    SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight,50);
        //    {
        //        if (SirenixEditorGUI.ToolbarButton(new GUIContent("È«²¿±£´æ")))
        //        {
        //            _weaponEditor.Save();
        //            TableOperation.ApplyAlter();
        //        }
        //    }
        //    SirenixEditorGUI.EndHorizontalToolbar();
        //}
    }
}

