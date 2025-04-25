using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace OdinEditor
{
    public class StarAttribute
    {
        private bool confirmDelete = false;

        [VerticalGroup("材质预览")]
        [InlineProperty, LabelWidth(80), LabelText("材质")]
        [InlineEditor(InlineEditorModes.LargePreview, Expanded = true)]
        public Material StarMaterial;

        [VerticalGroup("其他属性")]
        [InlineProperty, LabelWidth(100), LabelText("恒星对象")]
        public GameObject StarObject;

        [VerticalGroup("其他属性")]
        [LabelText("恒星着色器类型"),LabelWidth(100)]
        public StarShaderType ShaderType;

        [VerticalGroup("其他属性"),PropertySpace(80,0)]
        [Button(Name = "打开材质修改界面")]
        private void OpenMaterialModificationWindow()
        {
            var window = ScriptableObject.CreateInstance<StarMaterialModificateWindow>();
            window.Init(StarMaterial, StarObject, ShaderType);
            window.Show();
        }

        [VerticalGroup("其他属性")]
        [Button(Name = "删除材质")]
        private void DeleteMaterial()
        {
            confirmDelete = true;
        }

        [ShowIfGroup("其他属性/删除确认", Condition = "confirmDelete"),HorizontalGroup("其他属性/删除确认/确认")]
        [Button("DeleteConfirm", Name = "确定删除"), GUIColor(1, 0, 0)]
        private void DeleteConfirm()
        {
            if (StarMaterial != null)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(StarMaterial));
            if (StarObject != null)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(StarObject));

            confirmDelete = false;
        }

        [ShowIfGroup("其他属性/删除确认", Condition = "confirmDelete"),HorizontalGroup("其他属性/删除确认/确认")]
        [Button("DeleteConfirm", Name = "取消删除"), GUIColor(0, 1, 0)]
        private void DeleteCancel()
        {
            confirmDelete = false;
        }

        public StarAttribute(GameObject starObject)
        {
            StarObject = starObject;
            StarMaterial = starObject.GetComponent<Renderer>().sharedMaterial;

            if (StarMaterial != null)
            {
                //判断恒星着色器类型
                switch (StarMaterial.shader.name)
                {
                    case "InterStar":
                        ShaderType = StarShaderType.基础恒星; break;
                }
            }
            //防止Warning
            if (confirmDelete)
            {
                confirmDelete = false;
            }
        }
    }

    public enum StarShaderType
    {
        基础恒星,
        待添加
    }
}

