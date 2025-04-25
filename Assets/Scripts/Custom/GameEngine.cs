using ECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    void Awake()
    {
    }

    void Update()
    {
        TimerMgr.Instance.Loop(Time.deltaTime);
        KeyboardEventBinding.Instance.DetectionInput();
        MouseEventBlinding.Instance.DetectionInput();
        InfoPanelMgr.Instance.UpdatePosition();
        MasterEntity.Instance.Update();
    }

    private void OnApplicationQuit()
    {
        MasterEntity.Instance.Destroy();
    }
}
