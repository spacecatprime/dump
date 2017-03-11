using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Action;
using AdventureEngine.Factory;

namespace AdventureEngine.System
{
    /// <summary>
    /// contains the states of the game for the model
    /// </summary>
    public class GameEngine
    {
        private ActionBus m_actionBus = new ActionBus();
        private ComponentFactory m_componentFactory;
        private ActionBusByKey<Entity.EntityId, object> m_EntityActionBus = new ActionBusByKey<Entity.EntityId, object>();

        public GameEngine()
        {
            m_componentFactory = new ComponentFactory(this);
        }

        public void RegisterDefaultAdventureComponents()
        {
            DefaultComponentFactory.RegisterDefaultAdventureComponents(m_componentFactory);
        }

        public ActionBus SytemActionBus { get { return m_actionBus; } }
        public ComponentFactory ComponentFactory { get { return m_componentFactory; } }
        public ActionBusByKey<Entity.EntityId, object> EntityActionBus { get { return m_EntityActionBus; } }
    }
}
