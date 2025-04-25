using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    /// <summary>
    /// 能力冷却组件，时间单位为0.05s
    /// </summary>
    public class AbilityColdComponent : ECSComponent
    {
        public float ColdCD { get; set; } = 0;

        public override void Update()
        {
            base.Update();
            if (ColdCD > 0) { ColdCD -= Time.deltaTime; }
            else { ColdCD = 0; }
        }

        public void EnterCold(float coldTime)
        {
            ColdCD = coldTime;
        }


    }
}

