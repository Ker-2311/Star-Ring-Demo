using System;
using System.Collections;
using System.Collections.Generic;

namespace ECS
{
    public class ECSComponent
    {

        public Entity Entity { get; set; }
        public bool IsDisposed { get; set; }
        private bool enable = false;
        public bool Enable {
            get { return enable; }
            set 
            {
                if (enable == value) return;
                enable = value;
                if (enable) OnEnable();
                else OnDisable();
            } }

        public virtual bool DefaultEnable { get; set; } = true;

        public virtual void Awake()
        {

        }

        public virtual void Awake(object initData)
        {

        }

        public virtual void Setup()
        {

        }

        public virtual void Setup(object initData)
        {

        }

        public virtual void OnDestroy()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void Update()
        {
            
        }

        public virtual void OnEnable()
        {

        }

        public void Dispose()
        {
            Enable = false;
            IsDisposed = true;
        }

    }
}

