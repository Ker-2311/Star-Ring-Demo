using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace OdinEditor
{
    public class StarMaterialEditor
    {
        [LabelText("������ɫ��ѡ��")]
        [InlineButton("CreateMaterial", "��������")]
        public StarShaderType ShaderType;
        
        [TitleGroup("���ǲ����б�",order:1)]
        [TableList(AlwaysExpanded = true,IsReadOnly = true)]
        [ShowInInspector(),HideLabel()]
        public List<StarAttribute> Stars;

        //����Ԥ����
        private GameObject _starPrefab;

        public StarMaterialEditor()
        {
            Stars = new List<StarAttribute>();
            _starPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/StarMapSystem/Sun/A");
            //������к���
            foreach (var starObject in ResMgr.Instance.GetAllResources<GameObject>("Prefabs/StarMapSystem/Sun"))
            {
                var starAttribute = new StarAttribute(starObject);
                Stars.Add(starAttribute);
            }
        }

        private void CreateMaterial()
        {
            Material material;
            GameObject starObject = ResMgr.Instance.GetInstance(_starPrefab);

            var name = "�²���" + IDFactory.GenerateIdFormTime();
            
            switch (ShaderType)
            {
                case StarShaderType.��������:
                    material = new Material(Shader.Find("InterStar"));break;

                default: material = new Material(Shader.Find("InterStar"));break;
            }

            starObject.GetComponent<Renderer>().material = material;

            Stars.Add(new StarAttribute(starObject));
            AssetDatabase.CreateAsset(material, "Assets/Materials/StarMap/"+ name+".mat");
            PrefabUtility.SaveAsPrefabAsset(starObject, "Assets/Resources/Prefabs/StarMapSystem/Star/" + name+".prefab");

            GameObject.DestroyImmediate(starObject);

        }
    }
}

