using ECS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//视差滚动
public class BackgroundParallax : MonoBehaviour
{
    public Transform[] backgrounds;
    public float parallaxScale = 0.5f;
    public float parallaxReductionFactor = 0.4f;
    public float smoothing = 1f;

    private Camera _camera;
    private Vector3 _prePos;

    private void LateUpdate()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
            _prePos = _camera.transform.position;
            return;
        }
        //视差值计算
        var parallaxX = (_camera.transform.position.x - _prePos.x) * parallaxScale;
        var parallaxY = (_camera.transform.position.y - _prePos.y) * parallaxScale;

        for (int i = 0; i< backgrounds.Length;i++)
        {
            var bgTargetPosX = backgrounds[i].position.x + parallaxX * (i * parallaxReductionFactor + 1);
            var bgTargetPosY = backgrounds[i].position.y + parallaxY * (i * parallaxReductionFactor + 1);
            var bgTargetPos = new Vector3(bgTargetPosX, bgTargetPosY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smoothing);
        }

        _prePos = _camera.transform.position;
    }
}
