using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    /// <summary>
    /// ������ȴ�����ʱ�䵥λΪ0.05s
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

