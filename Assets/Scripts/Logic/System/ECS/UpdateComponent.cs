using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    /// <summary>
    /// 使挂载的组件允许执行Update函数
    /// </summary>
    public class UpdateComponent : ECSComponent
    {
        public override void Update()
        {
            base.Update();
            Entity.Update();
        }
    }
}

