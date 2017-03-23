using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;
using System.Collections;
using AdventureEngine.Entity;
using AdventureEngine.Bus;
using AdventureEngine.Model;

namespace AdventureEngine.Factory
{
    // TODO make an EntityContext class/struct

    /// <summary>
    /// create default entities or deserializes an entity from a stream
    /// </summary>
    public class EntityFactory
    {
        private Func<object, IEntity> m_factory;
        private ActionBus m_actionBus;

        private class DefaultEnity : AbstractEntity
        {
        }

        public EntityFactory(GameEngine gameEngine)
        {
            m_factory = context =>
            {
                return new DefaultEnity();
            };
            m_actionBus = gameEngine.SytemActionBus;
        }

        public IEntity CreateDefault(object context)
        {
            IEntity entity = m_factory(context);
            m_actionBus.Signal<InitEvent, EntityEvent>(new EntityEvent(entity));
            return entity;
        }

        public void SetDefaultEntityFactory(Func<object, IEntity> factory)
        {
            m_factory = factory;
        }
    }
}
