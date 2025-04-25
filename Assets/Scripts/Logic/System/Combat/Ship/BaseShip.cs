using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Combat
{
    public interface IBaseShip
    {
        public string ID { get; set; }
        public CombatEntity CombatEntity { get; set; }
    }
}

