using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinEditor
{
    public class StarMaterialModificateWindow : OdinEditorWindow
    {

        private StarShaderType _shaderType;

        [VerticalGroup("����Ԥ��")]
        [LabelText("����"),HideInInlineEditors]
        [InlineEditor(InlineEditorModes.LargePreview, Expanded = true)]
        public Material StarMaterial;

        [ShowIf("_shaderType",Value = StarShaderType.��������)]
        [ShowInInspector]
        [LabelText("����������ɫ��")]
        public BaseStarShader BaseStarShader = new BaseStarShader();

        public void Init(Material material,GameObject starObject,StarShaderType starShaderType)
        {
            StarMaterial = material;
            _shaderType = starShaderType;
            switch (starShaderType)
            {
                case StarShaderType.��������:BaseStarShader.Init(material, starObject);break;
            }
        }
    }
}

