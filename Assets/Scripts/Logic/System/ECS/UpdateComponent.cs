using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    /// <summary>
    /// ʹ���ص��������ִ��Update����
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

