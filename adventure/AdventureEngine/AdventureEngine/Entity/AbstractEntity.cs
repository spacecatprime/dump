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
        protected string m_uuid;
        protected List<IComponent> m_components = new List<IComponent>();
        protected List<IEntity> m_children = new List<IEntity>();
        protected IEntity m_parent = null;

        public AbstractEntity()
        {
            m_uuid = Guid.NewGuid().ToString();
        }

        List<IComponent> IEntity.Components
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

        List<IEntity> IEntity.GetChildren()
        {
            return m_children;
        }

        String IEntity.GetId()
        {
            return m_uuid;
        }

        IEntity IEntity.GetParent()
        {
            return m_parent;
        }
    }
}
