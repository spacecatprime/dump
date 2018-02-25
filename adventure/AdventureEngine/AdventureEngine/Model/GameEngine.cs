using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Bus;
using AdventureEngine.Factory;
using AdventureEngine.Interface;
using AdventureEngine.Entity;

namespace AdventureEngine.Model
{
    /// <summary>
    /// contains the states of the game for the model
    /// </summary>
    public class GameEngine
    {
        public class EntityBus : ActionBusByKey<Entity.EntityId, object>
        {
        }

        private ActionBus m_actionBus = new ActionBus();
        private EntityBus m_entityActionBus = new EntityBus();
        private List<IEntity> m_rootEntities = new List<IEntity>();
        private ComponentFactory m_componentFactory;
        private EntityFactory m_entityFactory;

        public GameEngine()
        {
            m_entityFactory = new EntityFactory(this);
            m_componentFactory = new ComponentFactory(this);
        }


        // adding a root object will automatically call "on start" to all entities
        public void AddRootEntity(IEntity root)
        {
            if (m_rootEntities.Contains(root))
            {
                throw new Exception("Already contains the root entity");
            }
            SytemActionBus.Signal<StartEvent, EntityEvent>(new EntityEvent(root));
            m_rootEntities.Add(root);
        }

        public void RemoveRootEntity(IEntity root)
        {
            if (m_rootEntities.Contains(root))
            {
                SytemActionBus.Signal<EndEvent, EntityEvent>(new EntityEvent(root));
                m_rootEntities.Remove(root);
            }
            throw new Exception("Could not find root entity to remove");
        }

        public IReadOnlyList<IEntity> Roots()
        {
            return m_rootEntities.AsReadOnly();
        }

        public ActionBus SytemActionBus { get { return m_actionBus; } }
        public ComponentFactory ComponentFactory { get { return m_componentFactory; } }
        public EntityBus EntityActionBus { get { return m_entityActionBus; } }
        public EntityFactory EntityFactory { get { return m_entityFactory; } }
    }
}
