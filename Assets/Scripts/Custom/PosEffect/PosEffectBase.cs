using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 屏幕后处理效果基类
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PosEffectBase : MonoBehaviour
{
    protected Material CheckShaderAndCreateMaterial(Shader shader, Material material)
    {
        if (shader == null)
        {
            return null;
        }

        if (shader.isSupported && material && material.shader == shader)
            return material;

        if (!shader.isSupported)
        {
            return null;
        }
        else
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            if (material)
                return material;
            else
                return null;
        }
    }

}
