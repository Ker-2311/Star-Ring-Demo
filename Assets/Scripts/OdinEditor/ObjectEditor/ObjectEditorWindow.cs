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
    /// ��Ϸ����༭��
    /// </summary>
    public class ObjectEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("�༭��/��Ϸ����༭��")]
        private static void Open()
        {
            var window = GetWindow<ObjectEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 400);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            //tree.Config.DrawSearchToolbar = true;

            tree.Add("���Ǳ༭��", new StarMaterialEditor());
            return tree;
        }
    }
}

