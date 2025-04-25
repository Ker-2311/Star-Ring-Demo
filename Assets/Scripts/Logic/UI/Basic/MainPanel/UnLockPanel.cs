using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockPanel : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown)
        {
            UnlockMgr.Instance.Pop();
        }
    }
}
