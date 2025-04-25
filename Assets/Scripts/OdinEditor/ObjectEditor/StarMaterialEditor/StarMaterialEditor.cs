using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace OdinEditor
{
    public class StarMaterialEditor
    {
        [LabelText("材质着色器选择")]
        [InlineButton("CreateMaterial", "创建材质")]
        public StarShaderType ShaderType;
        
        [TitleGroup("恒星材质列表",order:1)]
        [TableList(AlwaysExpanded = true,IsReadOnly = true)]
        [ShowInInspector(),HideLabel()]
        public List<StarAttribute> Stars;

        //恒星预制体
        private GameObject _starPrefab;

        public StarMaterialEditor()
        {
            Stars = new List<StarAttribute>();
            _starPrefab = ResMgr.Instance.GetResource<GameObject>("Prefabs/StarMapSystem/Sun/A");
            //添加所有恒星
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

            var name = "新材质" + IDFactory.GenerateIdFormTime();
            
            switch (ShaderType)
            {
                case StarShaderType.基础恒星:
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

