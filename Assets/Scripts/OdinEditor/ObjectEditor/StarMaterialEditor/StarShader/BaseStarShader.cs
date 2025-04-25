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

        [TitleGroup("������Ϣ")]
        [HorizontalGroup("������Ϣ/��������")]
        [LabelText("���ǲ�������"), ReadOnly]
        public string MaterialName;

        [HorizontalGroup("������Ϣ/��������")]
        [LabelText("���Ƕ�������")]
        [ReadOnly]
        public string StarObjectName;

        [TitleGroup("������ɫ������")]
        [HorizontalGroup("������ɫ������/��������",PaddingRight = 30)]
        [VerticalGroup("������ɫ������/��������/��������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("��������"),PreviewField]
        [AssetList(Path = "/Image/Noise")]
        public Texture BaseNoise;

        [VerticalGroup("������ɫ������/��������/��������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("��Ե��ƫ��"),Range(0,1)]
        public float RimBias;

        [VerticalGroup("������ɫ������/��������/��������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("��Ե���С"), Range(0, 4)]
        public float RimScale;

        [VerticalGroup("������ɫ������/��������/��������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("��Ե��Աȶ�"), Range(0, 1)]
        public float RimPower;

        [VerticalGroup("������ɫ������/��������/��������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("����ǿ��")]
        public float NoiseIntensity;

        [TitleGroup("����ƫ������")]
        [OnValueChanged("SetMaterial")]
        [LabelText("��������ƫ��")]
        public Vector4 BaseNoiseTilling;

        [TitleGroup("��ɫ����")]
        [HorizontalGroup("��ɫ����/��ɫ")]
        [OnValueChanged("SetMaterial")]
        [LabelText("������ɫ")]
        public Color BaseColor;

        [HorizontalGroup("��ɫ����/��ɫ")]
        [OnValueChanged("SetMaterial")]
        [LabelText("�ڻ���ɫ")]
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

