using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetection : PosEffectBase
{
    public Shader edgeDetectShader = null;
    private Material edgeDetectMaterial = null;
    public Material material
    {
        get
        {
            edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
            return edgeDetectMaterial;
        }
    }
    [Range(0.0f, 1.0f)]
    //当edgesOnly为0时，边缘将会叠加在原渲染图像上，为1时，则会只显示边缘，不显示原渲染图像  
    public float edgesOnly = 0.0f;
    public Color edgeColor = Color.black;
    public Color backgroundColor = Color.white;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_EdgeOnly", edgesOnly);
            material.SetColor("_EdgeColor", edgeColor);
            material.SetColor("_BackgroundColor", backgroundColor);

            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }  
}
