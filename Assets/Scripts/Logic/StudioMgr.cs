using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;
using UnityEngine.UI;

/// <summary>
/// π‹¿Ì…„œÒ≈Ô
/// </summary>
public class StudioMgr : MonoBehaviour
{
    public static StudioMgr instance;

    private Material _renderTarget;
    private void Awake()
    {
        instance = this;
        _renderTarget = ResMgr.Instance.GetResource<Material>("Materials/Stdio/StdioTarget");
    }

    public void AddObject(GameObject Object,Vector3 offset)
    {
        Object.transform.SetParent(transform, false);
        Object.transform.localPosition += offset;
    }

    public void SetTargetTexture(Image image)
    {
        image.material = _renderTarget;
    }

    public void ClearStudio()
    {
        gameObject.DestroyChilds();
    }
}
