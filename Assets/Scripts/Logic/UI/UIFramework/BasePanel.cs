using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exterior;

/// <summary>
/// �����࣬��Normal��
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
    /// ����ʱ����
    /// </summary>
    public virtual void OnEnter() 
    { 
        if(_panelAnimator !=null)
        {
            _panelAnimator.SetInteger("Action", 1);
        }
    }


    /// <summary>
    /// ����˳�ʱ����
    /// </summary>
    public virtual void OnExit()
    {
        if (_panelAnimator != null)
        {
            _panelAnimator.SetInteger("Action", 2);
        }
    }

    /// <summary>
    /// ��屻������帲��ʱ����
    /// </summary>
    public virtual void OnPause()
    {
        gameObject.FindOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    /// <summary>
    /// ���ָ���������״̬ʱ����
    /// </summary>
    public virtual void OnResume()
    {
        gameObject.FindOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
