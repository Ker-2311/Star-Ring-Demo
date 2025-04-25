using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotateControll : MonoBehaviour
{
    [Header("自转速度")]
    public float selfRotateSpeed = 1.0f;
    [Header("自转轴")]
    public Vector3 selfRoateAxes = Vector3.zero;

    private void Update()
    {
        transform.Rotate(selfRoateAxes, selfRotateSpeed * Time.deltaTime);
    }
}
