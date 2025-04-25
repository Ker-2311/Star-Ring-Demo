using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using ECS;
using System.IO;
using UnityEditor;
using ECS.Combat;
using BehaviorDesigner.Runtime;

public class testComponent:ECSComponent
{

}

public class testEntity : Entity
{
    public override void Awake()
    {
        base.Awake();
        this.AddComponent<testComponent>();
    }
}

[CreateAssetMenu(menuName ="test/createAsset")]
public class testScritpable:ScriptableObject
{
    public Effect str;

    public void Print()
    {
        Debug.Log(str);
    }
}


public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameMgr.Instance.SetupInit();
        //UIManager.instance.AddUI(ResMgr.instance.GetResource<GameObject>("Prefabs/UI/Basic/Main/MainPanel"), UIManager.UILayer.Top);
        GameMgr.Instance.LoadingInit();
        var bt = GetComponent<BehaviorTree>();
        bt.StartWhenEnabled = false;
        bt.RestartWhenComplete = true;
        bt.EnableBehavior();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
   
}
