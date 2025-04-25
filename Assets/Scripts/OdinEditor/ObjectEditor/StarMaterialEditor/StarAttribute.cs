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

        [VerticalGroup("����Ԥ��")]
        [InlineProperty, LabelWidth(80), LabelText("����")]
        [InlineEditor(InlineEditorModes.LargePreview, Expanded = true)]
        public Material StarMaterial;

        [VerticalGroup("��������")]
        [InlineProperty, LabelWidth(100), LabelText("���Ƕ���")]
        public GameObject StarObject;

        [VerticalGroup("��������")]
        [LabelText("������ɫ������"),LabelWidth(100)]
        public StarShaderType ShaderType;

        [VerticalGroup("��������"),PropertySpace(80,0)]
        [Button(Name = "�򿪲����޸Ľ���")]
        private void OpenMaterialModificationWindow()
        {
            var window = ScriptableObject.CreateInstance<StarMaterialModificateWindow>();
            window.Init(StarMaterial, StarObject, ShaderType);
            window.Show();
        }

        [VerticalGroup("��������")]
        [Button(Name = "ɾ������")]
        private void DeleteMaterial()
        {
            confirmDelete = true;
        }

        [ShowIfGroup("��������/ɾ��ȷ��", Condition = "confirmDelete"),HorizontalGroup("��������/ɾ��ȷ��/ȷ��")]
        [Button("DeleteConfirm", Name = "ȷ��ɾ��"), GUIColor(1, 0, 0)]
        private void DeleteConfirm()
        {
            if (StarMaterial != null)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(StarMaterial));
            if (StarObject != null)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(StarObject));

            confirmDelete = false;
        }

        [ShowIfGroup("��������/ɾ��ȷ��", Condition = "confirmDelete"),HorizontalGroup("��������/ɾ��ȷ��/ȷ��")]
        [Button("DeleteConfirm", Name = "ȡ��ɾ��"), GUIColor(0, 1, 0)]
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
                //�жϺ�����ɫ������
                switch (StarMaterial.shader.name)
                {
                    case "InterStar":
                        ShaderType = StarShaderType.��������; break;
                }
            }
            //��ֹWarning
            if (confirmDelete)
            {
                confirmDelete = false;
            }
        }
    }

    public enum StarShaderType
    {
        ��������,
        �����
    }
}

