using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
    public class Entity : IDisposable
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string InstanceID { get; set; }
        /// <summary>
        /// 是否已销毁
        /// </summary>
        public bool IsDisposed { get { return InstanceID == "0"; } private set { } }
        public Entity Parent { get; set; }
        public Dictionary<Type, List<Entity>> Childs = new Dictionary<Type, List<Entity>>() { };
        public Dictionary<Type, ECSComponent> Components = new Dictionary<Type, ECSComponent>() { };
        #region 实体状态
        public virtual void Awake()
        {

        }

        public virtual void Awake(object initData)
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Start(object initData)
        {

        }

        public virtual void OnSetParent(Entity preParent, Entity nowParent)
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnDestroy()
        {

        }

        public void Dispose()
        {
            if (Childs.Count > 0)
            {
                foreach (var childList in Childs.Values)
                {
                    for (int i = 0;i<childList.Count;i++)
                    {
                        EntityManager.Destroy(childList[i]);
                    }
                }
                Childs.Clear();
            }

            Parent?.RemoveChild(this);
            ClearComponents();
            Components.Clear();
            InstanceID = "0";
            if (MasterEntity.Instance.Entities.ContainsKey(GetType()))
            {
                MasterEntity.Instance.Entities[this.GetType()].Remove(this);
            }
        }

        #endregion
        #region 组件

        public T AddComponent<T>() where T : ECSComponent
        {
            if (Components.ContainsKey(typeof(T))) return Components[typeof(T)] as T;
            var component = Activator.CreateInstance<T>();
            component.Entity = this;
            component.IsDisposed = false;
            Components.Add(typeof(T), component);
            component.Awake();
            component.Setup();
            component.Enable = component.DefaultEnable;
            MasterEntity.Instance.AllComponents.Add(component);
            return component;
        }

        public T AddComponent<T>(object initData) where T : ECSComponent
        {
            if (Components.ContainsKey(typeof(T))) return Components[typeof(T)] as T;
            var component = Activator.CreateInstance<T>();
            component.Entity = this;
            component.IsDisposed = false;
            Components.Add(typeof(T), component);
            component.Awake(initData);
            component.Setup(initData);
            component.Enable = component.DefaultEnable;
            MasterEntity.Instance.AllComponents.Add(component);
            return component;
        }

        public void RemoveComponent(ECSComponent component)
        {
            if (component.Enable) component.Enable = false;
            component.OnDestroy();
            component.Dispose();
            MasterEntity.Instance.AllComponents.Remove(component);
            Components.Remove(component.GetType());
        }

        public void RemoveComponent<T>() where T : ECSComponent
        {
            if (Components[typeof(T)] == null) return;
            RemoveComponent(Components[typeof(T)]);
        }

        public void ClearComponents()
        {
            var keys = new List<Type>();
            foreach (var key in Components.Keys)
            {
                keys.Add(key);
            }
            foreach (var key in keys)
            {
                RemoveComponent(Components[key]);
            }
        }

        public T GetComponent<T>() where T : ECSComponent
        {
            if (Components.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }

        /// <summary>
        /// 组件存在?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool ExistComponent<T>() where T : ECSComponent
        {
            return Components.TryGetValue(typeof(T), out var component);
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : ECSComponent
        {
            if (Components.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }

        /// <summary>
        /// 尝试获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool TryGet<T>(out T component) where T : ECSComponent
        {
            if (Components.TryGetValue(typeof(T), out var c))
            {
                component = c as T;
                return true;
            }
            component = null;
            return false;
        }

        public bool TryGet<T, T1>(out T component, out T1 component1) where T : ECSComponent where T1 : ECSComponent
        {
            component = null;
            component1 = null;
            if (Components.TryGetValue(typeof(T), out var c)) component = c as T;
            if (Components.TryGetValue(typeof(T1), out var c1)) component1 = c1 as T1;
            if (component != null && component1 != null) return true;
            return false;
        }

        public bool TryGet<T, T1, T2>(out T component, out T1 component1, out T2 component2) where T : ECSComponent 
            where T1 : ECSComponent where T2 : ECSComponent
        {
            component = null;
            component1 = null;
            component2 = null;
            if (Components.TryGetValue(typeof(T), out var c)) component = c as T;
            if (Components.TryGetValue(typeof(T1), out var c1)) component1 = c1 as T1;
            if (Components.TryGetValue(typeof(T2), out var c2)) component2 = c2 as T2;
            if (component != null && component1 != null && component2 != null) return true;
            return false;
        }
        #endregion
        #region 子实体操作
        private void SetParent(Entity parent)
        {
            var preParent = Parent;
            this.Parent = parent;
            if (!parent.Childs.ContainsKey(this.GetType()))
            {
                parent.Childs.Add(this.GetType(), new List<Entity>());
            }
            else
            {
                //重复的子实体
                var repeatChild = parent.Childs[this.GetType()].Find((x) => x.ID == this.ID);
                if (repeatChild != null) return;
            }
            parent.Childs[this.GetType()].Add(this);
            OnSetParent(preParent, parent);
        }

        public void SetChild(Entity child)
        {
            child.SetParent(this);
        }

        public T GetParent<T>() where T : Entity
        {
            return Parent as T;
        }

        public void RemoveChild(Entity child)
        {
            if (child != null && Childs.TryGetValue(child.GetType(),out var childsList))
            {
                childsList.Remove(child);
                if (childsList.Count == 0)
                {
                    Childs.Remove(child.GetType());
                }
            }
        }

        public T AddChild<T>() where T : Entity
        {
            var entity = EntityManager.Create(typeof(T));
            entity.SetParent(this);
            return entity as T;
        }

        public T AddChild<T>(object initData) where T : Entity
        {
            var entity = EntityManager.Create(typeof(T),initData);
            entity.SetParent(this);
            return entity as T;
        }

        public T GetChild<T>() where T : Entity
        {
            if (Childs.ContainsKey(typeof(T)))
            {
                return Childs[typeof(T)].First() as T;
            }
            return null;
        }

        public T GetChild<T>(string id) where T:Entity
        {
            foreach (var childs in Childs.Values)
            {
                foreach (var child in childs)
                {
                    if (child.ID == id)
                    {
                        return child as T;
                    }
                }
            }
            return null;
        }

        public T Find<T>(string name) where T : Entity
        {
            if (Childs.TryGetValue(typeof(T), out var chidren))
            {
                foreach (var item in chidren)
                {
                    if (item.Name == name) return item as T;
                }
            }
            return null;
        }

        #endregion
    }
}

