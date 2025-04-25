using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class SingletonEntity<T> : Entity where T:Entity,new()
    {
        public static T Instance { get; set; }

    }
}

