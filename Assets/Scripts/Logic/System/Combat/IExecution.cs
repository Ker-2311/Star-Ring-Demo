using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public interface IExecution
    {
        public Entity AbilityEntity { get; set; }
        public CombatEntity OwnerEntity { get; set; }
        public CombatEntity InputTarget { get; set; }
        public Vector2 InputPoint { get; set; }
        public Vector2 InputDirection { get; set; }

        /// 开始执行
        public void BeginExecute();

        /// 结束执行
        public void EndExecute();
    }
}

