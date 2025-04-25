using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBloom : PosEffectBase
{
    //�ֱ���
    public int downSample = 1;
    //������
    public int samplerScale = 1;
    //����������ȡ��ֵ
    public Color colorThreshold = Color.gray;
    //Bloom������ɫ
    public Color bloomColor = Color.white;
    //BloomȨֵ
    [Range(0.0f, 1.0f)]
    public float bloomFactor = 0.5f;

    public Shader bloomShader;
    private Material blooMaterial = null;
    public Material material
    {
        get
        {
            blooMaterial = CheckShaderAndCreateMaterial(bloomShader, blooMaterial);
            return blooMaterial;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material)
        {
            //��������RT�����ҷֱ��ʰ���downSameple����
            RenderTexture temp1 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);
            RenderTexture temp2 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0, source.format);

            //ֱ�ӽ�����ͼ�������ͷֱ��ʵ�RT�ϴﵽ���ֱ��ʵ�Ч��
            Graphics.Blit(source, temp1);


            //������ֵ��ȡ��������,ʹ��pass0���и�����ȡ
            material.SetVector("_colorThreshold", colorThreshold);
            Graphics.Blit(temp1, temp2, material, 0);

            //��˹ģ��������ģ������������ʹ��pass1���и�˹ģ��
            material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
            Graphics.Blit(temp2, temp1, material, 1);
            material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
            Graphics.Blit(temp1, temp2, material, 1);

            //Bloom����ģ�����ͼ��ΪMaterial��Blurͼ����
            material.SetTexture("_BlurTex", temp2);
            material.SetVector("_bloomColor", bloomColor);
            material.SetFloat("_bloomFactor", bloomFactor);

            //ʹ��pass2���о���Ч�����㣬��������ͼֱ�Ӵ�source���뵽shader��_MainTex��
            Graphics.Blit(source, destination, material, 2);

            //�ͷ������RT
            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);
        }
    }

}
