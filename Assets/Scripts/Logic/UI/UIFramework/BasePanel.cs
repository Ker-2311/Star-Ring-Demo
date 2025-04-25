using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;

/// <summary>
/// 面板基类，在Normal层
/// </summary>
public abstract class BasePanel:MonoBehaviour
{
    private Animator _panelAnimator;

    public virtual void Awake()
    {
        _panelAnimator = GetComponent<Animator>();
    }

    public virtual void Init()
    {

    }
    /// <summary>
    /// 面板打开时调用
    /// </summary>
    public virtual void OnEnter() 
    { 
        if(_panelAnimator !=null)
        {
            _panelAnimator.SetInteger("Action", 1);
        }
    }


    /// <summary>
    /// 面板退出时调用
    /// </summary>
    public virtual void OnExit()
    {
        if (_panelAnimator != null)
        {
            _panelAnimator.SetInteger("Action", 2);
        }
    }

    /// <summary>
    /// 面板被其它面板覆盖时调用
    /// </summary>
    public virtual void OnPause()
    {
        gameObject.FindOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    /// <summary>
    /// 面板恢复不被覆盖状态时调用
    /// </summary>
    public virtual void OnResume()
    {
        gameObject.FindOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
