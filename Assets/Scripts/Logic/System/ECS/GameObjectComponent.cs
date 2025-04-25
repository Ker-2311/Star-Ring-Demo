using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public sealed class GameObjectComponent : ECSComponent
    {
        public UnityEngine.GameObject GameObject { get; private set; }

        public override void Awake()
        {
            GameObject = new UnityEngine.GameObject(Entity.GetType().Name);
            var view = GameObject.AddComponent<ComponentView>();
            view.Type = GameObject.name;
            view.Component = this;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnityEngine.GameObject.Destroy(GameObject);
        }

        public void OnNameChanged(string name)
        {
            GameObject.name = $"{Entity.GetType().Name}: {name}";
        }

        public void OnAddComponent(ECSComponent component)
        {
            var view = GameObject.AddComponent<ComponentView>();
            view.Type = component.GetType().Name;
            view.Component = component;
        }

        public void OnRemoveComponent(ECSComponent component)
        {
            var comps = GameObject.GetComponents<ComponentView>();
            foreach (var item in comps)
            {
                if (item.Component == component)
                {
                    UnityEngine.GameObject.Destroy(item);
                }
            }
        }

        public void OnAddChild(Entity child)
        {
            if (child.GetComponent<GameObjectComponent>() != null)
            {
                child.GetComponent<GameObjectComponent>().GameObject.transform.SetParent(GameObject.transform);
            }
        }
    }
}
