using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    [Header("旋转对象")]
    public GameObject rotateObject;
    [Header("旋转速度")]
    public float rotateSpeed = 1.0f;
    private float _rotateAngle = 0;
    private float _radius;
    private Vector3 _originPos;

    private void Awake()
    {
        _originPos = transform.position;
        var dir = new Vector2(_originPos.x - rotateObject.transform.position.x, _originPos.z - rotateObject.transform.position.z);
        _radius = dir.magnitude;
    }

    private void Update()
    {
        if (rotateObject != null)
        {
            transform.position = _originPos + new Vector3(_radius * Util.RadiansCos(_rotateAngle), 0, _radius * Util.RadiansSin(_rotateAngle));
            _rotateAngle += rotateSpeed * Time.deltaTime;
        }
    }
}
