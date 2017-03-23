using AdventureEngine.Bus;
using AdventureEngine.Interface;
using AdventureEngine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Entity
{
    public class BaseEntityEvent
    {
        private Action<IComponent> handleComponet;

        public BaseEntityEvent(Action<IComponent> handler)
        {
            handleComponet = handler;
        }

        public void OnEntityEvent(EntityEvent data)
        {
            if (data.Target == null)
            {
                throw new Exception("Should have a target");
            }
            SignalTree(data.Target, data.IncludeChildren);
        }

        protected void SignalTree(IEntity entity, bool includeChildren)
        {
            // start all components
            foreach (var c in entity.Components)
            {
                handleComponet.DynamicInvoke(c);
            }

            // go down the kid tree
            if (includeChildren)
            {
                var children = entity.GetChildren();
                foreach (var child in children)
                {
                    SignalTree(child, includeChildren);
                }
            }
        }
    }

    /// <summary>
    /// An event that runs the "OnStart" method on a target entity;
    /// optionally calls OnStart on all children components
    /// </summary>
    public class StartEvent
    {
        static internal void OnStartEntityEvent(EntityEvent data)
        {
            BaseEntityEvent handler = new BaseEntityEvent(c => c.OnStart());
            handler.OnEntityEvent(data);
        }
    }

    public class EndEvent
    {
        static internal void OnStopEntityEvent(EntityEvent data)
        {
            BaseEntityEvent handler = new BaseEntityEvent(c => c.OnEnd());
            handler.OnEntityEvent(data);
        }
    }

    public class InitEvent
    {
        static internal void OnInitEntityEvent(EntityEvent data)
        {         
            if (data.Target == null)
            {
                throw new Exception("Should have a target");
            }
            InitComponents(data.Target, data.IncludeChildren);
        }

        private static void InitComponents(IEntity entity, bool includeChildren)
        {
            // start all components
            foreach (var c in entity.Components)
            {
                c.OnInit(entity);
            }

            // go down the kid tree
            if (includeChildren)
            {
                var children = entity.GetChildren();
                foreach (var child in children)
                {
                    InitComponents(child, includeChildren);
                }
            }
        }
    }
}
