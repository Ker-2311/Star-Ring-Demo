using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotateControll : MonoBehaviour
{
    [Header("��ת�ٶ�")]
    public float selfRotateSpeed = 1.0f;
    [Header("��ת��")]
    public Vector3 selfRoateAxes = Vector3.zero;

    private void Update()
    {
        transform.Rotate(selfRoateAxes, selfRotateSpeed * Time.deltaTime);
    }
}
