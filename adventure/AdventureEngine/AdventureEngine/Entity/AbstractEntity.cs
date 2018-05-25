using AdventureEngine.Component;
using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Entity
{
    public abstract class AbstractEntity : IEntity
    {
        protected EntityId m_uuid;
        protected List<IComponent> m_components = new List<IComponent>();
        protected List<IEntity> m_children = new List<IEntity>();
        protected IEntity m_parent = null;

        public AbstractEntity()
        {
            m_uuid = new EntityId(Guid.NewGuid());
        }

        List<IComponent> IEntity.Components
        {
            get { return m_components; }
        }

        List<IEntity> IEntity.GetChildren()
        {
            return m_children;
        }

        EntityId IEntity.GetId()
        {
            return m_uuid;
        }

        IEntity IEntity.GetParent()
        {
            return m_parent;
        }

        object IEntity.GetAttribute(string name)
        {
            foreach (var comp in m_components)
            {
                if (comp is AttributeBag)
                {
                    string[] pair = name.Split('.');
                    AttributeBag bag = comp as AttributeBag;
                    if (bag.Name == pair[0])
                    {
                        return bag.GetValue(pair[1]);
                    }
                }
            }
            return null;
        }

        object IEntity.SetAttribute(string name, object value)
        {
            foreach (var comp in m_components)
            {
                if (comp is AttributeBag)
                {
                    string[] pair = name.Split('.');
                    AttributeBag bag = comp as AttributeBag;
                    if (bag.Name == pair[0])
                    {
                        return bag.SetValue(pair[1], value);
                    }
                }
            }
            return null;
        }
    }
}
