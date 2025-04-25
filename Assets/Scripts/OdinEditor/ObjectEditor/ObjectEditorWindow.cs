using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OdinEditor
{
    /// <summary>
    /// ÓÎÏ·¶ÔÏó±à¼­Æ÷
    /// </summary>
    public class ObjectEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("±à¼­Æ÷/ÓÎÏ·¶ÔÏó±à¼­Æ÷")]
        private static void Open()
        {
            var window = GetWindow<ObjectEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 400);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            //tree.Config.DrawSearchToolbar = true;

            tree.Add("ºãÐÇ±à¼­Æ÷", new StarMaterialEditor());
            return tree;
        }
    }
}

