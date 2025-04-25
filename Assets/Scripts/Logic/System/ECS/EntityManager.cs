using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public static class EntityManager
    {
        public static MasterEntity Master => MasterEntity.Instance;
        private static Entity NewEntity(Type entityType, string id = "0")
        {
            var entity = Activator.CreateInstance(entityType) as Entity;
            entity.InstanceID = IDFactory.GenerateIdFormTime();
            if (id == "0") entity.ID = entity.InstanceID;
            else entity.ID = id;
            if (!Master.Entities.ContainsKey(entityType))
            {
                Master.Entities.Add(entityType, new List<Entity>());
            }
            Master.Entities[entityType].Add(entity);
            return entity;
        }

        public static Entity Create(Type entityType)
        {
            var entity = NewEntity(entityType);
            SetupEntity(entity);
            return entity;
        }

        public static Entity Create(Type entityType, object initData)
        {
            var entity = NewEntity(entityType);
            SetupEntity(entity, initData);
            return entity;
        }

        public static T Create<T>() where T : Entity
        {
            return Create(typeof(T)) as T;
        }

        public static T Create<T>(object initData) where T : Entity
        {
            return Create(typeof(T), initData) as T;
        }

        private static void SetupEntity(Entity entity)
        {
            //parent.SetChild(entity);
            entity.Awake();
            entity.Start();
        }

        private static void SetupEntity(Entity entity, object initData)
        {
            //parent.SetChild(entity);
            entity.Awake(initData);
            entity.Start(initData);
        }

        public static void Destroy(Entity entity)
        {
            entity.OnDestroy();
            entity.Dispose();
        }

    }
}

