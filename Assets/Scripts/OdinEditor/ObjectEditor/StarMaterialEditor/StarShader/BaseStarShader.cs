using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OdinEditor
{
    public class BaseStarShader
    {
        private Material _material;
        private GameObject _starObject;

        [TitleGroup("材质信息")]
        [HorizontalGroup("材质信息/名字设置")]
        [LabelText("恒星材质名字"), ReadOnly]
        public string MaterialName;

        [HorizontalGroup("材质信息/名字设置")]
        [LabelText("恒星对象名字")]
        [ReadOnly]
        public string StarObjectName;

        [TitleGroup("基本着色器设置")]
        [HorizontalGroup("基本着色器设置/属性设置",PaddingRight = 30)]
        [VerticalGroup("基本着色器设置/属性设置/纹理设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("基本噪音"),PreviewField]
        [AssetList(Path = "/Image/Noise")]
        public Texture BaseNoise;

        [VerticalGroup("基本着色器设置/属性设置/纹理设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("边缘光偏移"),Range(0,1)]
        public float RimBias;

        [VerticalGroup("基本着色器设置/属性设置/纹理设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("边缘光大小"), Range(0, 4)]
        public float RimScale;

        [VerticalGroup("基本着色器设置/属性设置/纹理设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("边缘光对比度"), Range(0, 1)]
        public float RimPower;

        [VerticalGroup("基本着色器设置/属性设置/纹理设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("纹理强度")]
        public float NoiseIntensity;

        [TitleGroup("纹理偏移设置")]
        [OnValueChanged("SetMaterial")]
        [LabelText("基本纹理偏移")]
        public Vector4 BaseNoiseTilling;

        [TitleGroup("颜色设置")]
        [HorizontalGroup("颜色设置/颜色")]
        [OnValueChanged("SetMaterial")]
        [LabelText("基本颜色")]
        public Color BaseColor;

        [HorizontalGroup("颜色设置/颜色")]
        [OnValueChanged("SetMaterial")]
        [LabelText("内环颜色")]
        public Color InnerRimColor;



        public void Init(Material material,GameObject starObject)
        {
            _material = material;
            _starObject = starObject;
            MaterialName = material.name;
            StarObjectName = starObject.name;
            BaseNoise = material.GetTexture("_BaseNoise");
            BaseColor = material.GetColor("_BaseColor");
            InnerRimColor = material.GetColor("_InnerRimColor");
            RimBias = material.GetFloat("_RimBias");
            RimScale = material.GetFloat("_RimScale");
            RimPower = material.GetFloat("_RimPower");
            NoiseIntensity = material.GetFloat("_NoiseIntensity");
            BaseNoiseTilling = material.GetVector("_BaseNoiseTilling");
        }

        private void SetMaterial()
        {
            _material.SetTexture("_BaseNoise",BaseNoise);
            _material.SetColor("_BaseColor", BaseColor);
            _material.SetColor("_InnerRimColor", InnerRimColor);
            _material.SetFloat("_RimBias", RimBias);
            _material.SetFloat("_RimScale", RimScale);
            _material.SetFloat("_RimPower",RimPower);
            _material.SetFloat("_NoiseIntensity",NoiseIntensity);
            _material.SetVector("_BaseNoiseTilling", BaseNoiseTilling);
        }
    }
}

