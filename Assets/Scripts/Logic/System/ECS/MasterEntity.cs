using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    /// <summary>
    /// ��ʵ�壬�������������ʵ��
    /// </summary>
    public class MasterEntity : Entity
    {
        public static MasterEntity Instance { get; private set; } = new MasterEntity();
        public Dictionary<Type, List<Entity>> Entities { get; private set; } = new Dictionary<Type, List<Entity>>();
        public List<ECSComponent> AllComponents { get; private set; } = new List<ECSComponent>();

        public void Destroy()
        {
            EntityManager.Destroy(Instance);
            Instance = null;
        }

        public override void Update()
        {
            //ִ�����Update
            for (int i = AllComponents.Count - 1; i >= 0; i--)
            {
                var item = AllComponents[i];
                if (item.IsDisposed)
                {
                    AllComponents.RemoveAt(i);
                    continue;
                }
                if (!item.Enable)
                {
                    continue;
                }
                item.Update();
            }
        }
    }
}

