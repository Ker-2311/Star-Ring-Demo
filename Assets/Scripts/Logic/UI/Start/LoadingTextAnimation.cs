using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTextAnimation : MonoBehaviour
{
    public void Enter()
    {

    }
    
    public void Exit()
    {
        TimerMgr.Instance.CreateTimerAndStart(1, 1, () =>
          {
              Debug.Log("É¾³ý");
              GameObject.Destroy(gameObject);
          });
    }
}
