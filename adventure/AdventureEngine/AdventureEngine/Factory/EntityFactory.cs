using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;
using System.Collections;
using AdventureEngine.Entity;

namespace AdventureEngine.Factory
{
    /// <summary>
    /// create default entities or deserializes an entity from a stream
    /// </summary>
    public class EntityFactory
    {
        private Func<object, IEntity> m_factory;

        private class DefaultEnity : IEntity
        {
            private List<IComponent> m_components = new List<IComponent>();
            private List<IEntity> m_children = new List<IEntity>();
            private IEntity m_parent = null;
            private EntityId m_uid;

            public DefaultEnity()
            {
                m_uid = new EntityId(Guid.NewGuid());
            }

            public List<IComponent> Components
            {
                get
                {
                    return m_components;
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public List<IEntity> GetChildren()
            {
                return m_children;
            }

            public EntityId GetId()
            {
                return m_uid;
            }

            public IEntity GetParent()
            {
                return m_parent;
            }
        }

        public EntityFactory()
        {
            m_factory = context =>
            {
                return new DefaultEnity();
            };
        }

        public IEntity CreateDefault(object context)
        {
            return m_factory(context);
        }

        public void SetDefaultEntityFactory(Func<object, IEntity> factory)
        {
            m_factory = factory;
        }
    }
}
