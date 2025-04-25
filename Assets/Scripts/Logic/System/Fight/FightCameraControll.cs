using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCameraControll : MonoBehaviour
{
    //跟随目标
    public Transform Target;
    //移动平滑度
    public float Smooth = 3.5f;
    //缩放速度
    public float ZoomSpeed = 200f;
    //转动速度
    public float RotationSpeed = 10;
    //移动速度
    public float MoveSpeed = 100;
    //相机偏置
    public Vector3 PosOffset = new Vector3(0, 0, 0);
    public Vector3 RotOffset = new Vector3(0, 0, 0);

    private Vector3 _posOffsetCache;
    public float MouseOffsetSmoothing = 0.1f;

    private Vector3 _targetPosCache;
    private void FixedUpdate()
    {
        if(Target != null)
        {
            CameraUpdate(_targetPosCache);
            _targetPosCache = Target.position;
        }

    }

    private void CameraUpdate(Vector3 targetPos)
    {
        //相机平滑移动
        var endPosition = targetPos + PosOffset;
        var endRotation = Quaternion.Euler(RotOffset);

        transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * Smooth);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, Time.deltaTime * Smooth);
    }

    /// <summary>
    /// 设置相机跟随目标
    /// </summary>
    /// <param name="target"></param>
    /// <param name="canZoom"></param>
    /// <param name="canRotate"></param>
    /// <param name="canMove"></param>
    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void SetTarget(Transform target, Vector3 offsetPos, Vector3 offsetRot)
    {
        SetTarget(target);
        PosOffset = offsetPos;
        RotOffset = offsetRot;
        _posOffsetCache = PosOffset;
    }

    /// <summary>
    /// 鼠标偏移
    /// </summary>
    /// <param name="mousePos"></param>
    public void MouseOffset(Vector2 mousePos)
    {
        var offset = new Vector3((mousePos.x - (Screen.width / 2)), (mousePos.y - (Screen.height / 2)), 0) * MouseOffsetSmoothing;
        PosOffset = _posOffsetCache + offset;
    }

    public void EndMouseOffset()
    {
        PosOffset = _posOffsetCache;
    }
}
