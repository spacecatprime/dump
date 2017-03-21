using AdventureEngine.Bus;
using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Entity
{
    /// <summary>
    /// An event that runs the "OnStart" method on a target entity;
    /// optionally calls OnStart on all children components
    /// </summary>
    public class StartEvent
    {
        static internal void OnStartEntityEvent(EntityEvent data)
        {
            if (data.Target == null)
            {
                throw new Exception("Should have a target");
            }
            StartComponents(data.Target, data.IncludeChildren);
        }

        private static void StartComponents(IEntity entity, bool includeChildren)
        {
            // start all components
            foreach (var c in entity.Components)
            {
                c.OnStart();
            }

            // go dow the kid tree
            if(includeChildren)
            {
                var children = entity.GetChildren();
                foreach (var child in children)
                {
                    StartComponents(child, includeChildren);
                }
            }
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

            // go dow the kid tree
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
