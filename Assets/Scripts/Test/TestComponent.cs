using ECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : ECSComponent
{
    public override void Awake()
    {
        base.Awake();
        Debug.Log("(Awake)����һ���������");
    }
}
