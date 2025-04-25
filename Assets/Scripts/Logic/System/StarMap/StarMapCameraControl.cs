using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMapCameraControl : MonoBehaviour
{
    //����Ŀ��
    public Transform Target;
    //�ƶ�ƽ����
    public float Smooth = 3.5f;
    //�����ٶ�
    public float ZoomSpeed = 200f;
    //ת���ٶ�
    public float RotationSpeed = 10;
    //�ƶ��ٶ�
    public float MoveSpeed = 100;
    //���ƫ��
    public Vector3 PosOffset = new Vector3(0,0,0);
    public Vector3 RotOffset = new Vector3(0,0,0);
    //��ֱ�Ƕ�
    public float MaxRot = 25;
    public float MinRot = 0;
    //������ž���
    public float MaxPos = 100;
    //����ƶ��뾶
    public float MaxRadius = 200;

    //�Ƿ�����������
    public bool CanZoom = true;
    public bool CanRotate = true;
    public bool CanMove = true;

    //�����ת
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
            //���ƽ���ƶ�
            var endPosition = Target.position + PosOffset;
            var endRotation = Quaternion.Euler(Target.localEulerAngles + RotOffset);

            transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * Smooth);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, Time.deltaTime * Smooth);
        }
    }

    /// <summary>
    /// ��������ƶ�����
    /// </summary>
    /// <param name="target">���������Ŀ������ƶ�</param>
    /// <param name="endPosition">�ƶ����յ�(������target�յ���ͬ)</param>
    /// <param name="finalPosition">���ƶ�����������ƶ�����λ��</param>
    public void CameraMoveAnimation(Transform target,Vector3 endPosition,Vector3 finalPosition)
    {
        SetTarget(target, false, false, false);
        TimerMgr.Instance.CreateTimerAndStart(5, 1, () =>
        {
            Debug.LogWarning("����ƶ�ʱ�����������targetĿ�ĵغ�endPosition�Ƿ����");
            return;
        });
        while (Vector3.Distance(endPosition, transform.position) >= 1f) ;
        transform.position = finalPosition;
        Target = null;
    }

    /// <summary>
    /// �����������Ŀ��
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
    /// �����������
    /// </summary>
    public void CameraZoom()
    {
        var zoomValue = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        //������������ʱ
        if (zoomValue != 0 && CanZoom)
        {
            //�ж��Ƿ�Խ��
            if (Mathf.Abs(zoomValue + _curPos) <= MaxPos)
            {
                _curPos += zoomValue;
                PosOffset += _originForward * zoomValue;
            }
        }
    }

    /// <summary>
    /// �����ת
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
            //�ж��Ƿ�Խ��
            if (moveDelta.magnitude <= MaxRadius)
            {
                PosOffset += moveDir;
            }
        }
    }
}
