using AdventureEngine.Bus;
using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Entity
{
    public class DefaultEntityEvents
    {
        static public void RegisterForDefaultAdventureEngine(ActionBus actionBus)
        {
            actionBus.Subscribe<StartEvent, EntityEvent>((data) => StartEvent.OnStartEntityEvent(data));
            actionBus.Subscribe<InitEvent, EntityEvent>((data) => InitEvent.OnInitEntityEvent(data));
            actionBus.Subscribe<EndEvent, EntityEvent>((data) => EndEvent.OnStopEntityEvent(data));
        }
    }

    /// <summary>
    /// Common entity event object signal input
    /// </summary>
    public class EntityEvent
    {
        public IEntity Target { get; set; }
        public object Input { get; set; }
        public bool IncludeChildren { get; set; }

        public EntityEvent(IEntity e, object i = null, bool include = true)
        {
            Target = e;
            Input = i;
            IncludeChildren = include;
        }
    }
}
