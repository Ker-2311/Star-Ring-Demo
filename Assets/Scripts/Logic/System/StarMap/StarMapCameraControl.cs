using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMapCameraControl : MonoBehaviour
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
    public Vector3 PosOffset = new Vector3(0,0,0);
    public Vector3 RotOffset = new Vector3(0,0,0);
    //垂直角度
    public float MaxRot = 25;
    public float MinRot = 0;
    //最大缩放距离
    public float MaxPos = 100;
    //最大移动半径
    public float MaxRadius = 200;

    //是否可以相机操作
    public bool CanZoom = true;
    public bool CanRotate = true;
    public bool CanMove = true;

    //相机反转
    public bool IsReverse = false;

    private float _curPos = 0;
    private Vector3 _originForward = Vector3.zero;


    private void Awake() 
    {
        
    }

    private void FixedUpdate()
    {
        CameraZoom();
        CameraRotate();
        CameraMove();
        if (Target != null)
        {
            //相机平滑移动
            var endPosition = Target.position + PosOffset;
            var endRotation = Quaternion.Euler(Target.localEulerAngles + RotOffset);

            transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * Smooth);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, Time.deltaTime * Smooth);
        }
    }

    /// <summary>
    /// 进行相机移动动画
    /// </summary>
    /// <param name="target">相机会跟随该目标进行移动</param>
    /// <param name="endPosition">移动的终点(必须与target终点相同)</param>
    /// <param name="finalPosition">在移动结束后将相机移动到该位置</param>
    public void CameraMoveAnimation(Transform target,Vector3 endPosition,Vector3 finalPosition)
    {
        SetTarget(target, false, false, false);
        TimerMgr.Instance.CreateTimerAndStart(5, 1, () =>
        {
            Debug.LogWarning("相机移动时间过长，请检查target目的地和endPosition是否相等");
            return;
        });
        while (Vector3.Distance(endPosition, transform.position) >= 1f) ;
        transform.position = finalPosition;
        Target = null;
    }

    /// <summary>
    /// 设置相机跟随目标
    /// </summary>
    /// <param name="target"></param>
    /// <param name="canZoom"></param>
    /// <param name="canRotate"></param>
    /// <param name="canMove"></param>
    public void SetTarget(Transform target,bool canZoom = true, bool canRotate = true, bool canMove = true)
    {
        Target = target;
        _originForward = target.forward + PosOffset;
        _curPos = 0;
        CanZoom = canZoom;
        CanRotate = canRotate;
        CanMove = canMove;
    }
    public void SetTarget(Transform target,Vector3 offsetPos,Vector3 offsetRot
        , bool canZoom = true, bool canRotate = true, bool canMove = true)
    {
        SetTarget(target, canZoom, canRotate, canMove);
        PosOffset = offsetPos;
        RotOffset = offsetRot;
    }

    /// <summary>
    /// 控制相机缩放
    /// </summary>
    public void CameraZoom()
    {
        var zoomValue = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        //当滚轮有输入时
        if (zoomValue != 0 && CanZoom)
        {
            //判断是否越界
            if (Mathf.Abs(zoomValue + _curPos) <= MaxPos)
            {
                _curPos += zoomValue;
                PosOffset += _originForward * zoomValue;
            }
        }
    }

    /// <summary>
    /// 相机旋转
    /// </summary>
    public void CameraRotate()
    {
        if (Input.GetMouseButton(1) && CanRotate)
        {
            if (IsReverse)
            {
                RotOffset.y += -Input.GetAxis("Mouse X") * RotationSpeed;
                RotOffset.x += -Input.GetAxis("Mouse Y") * RotationSpeed;
            }
            else
            {
                RotOffset.y += Input.GetAxis("Mouse X") * RotationSpeed;
                RotOffset.x += -Input.GetAxis("Mouse Y") * RotationSpeed;
            }
            RotOffset.x = Mathf.Clamp(RotOffset.x, MinRot, MaxRot);
        }
    }

    public void CameraMove()
    {
        if (Input.GetMouseButton(2) && CanMove)
        {
            var moveValue_x = -Input.GetAxis("Mouse X") * MoveSpeed;
            var moveValue_y = -Input.GetAxis("Mouse Y") * MoveSpeed;
            var moveDir = moveValue_x * transform.right + moveValue_y * transform.up;
            var moveDelta = new Vector2((moveDir + PosOffset).x, (moveDir + PosOffset).z);
            //判断是否越界
            if (moveDelta.magnitude <= MaxRadius)
            {
                PosOffset += moveDir;
            }
        }
    }
}
