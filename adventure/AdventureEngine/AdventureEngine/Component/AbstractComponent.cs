using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    /// <summary>
    /// Creates a logical chunk for each Entity
    /// </summary>
    public abstract class AbstractComponent : IComponent
    {
        protected IEntity m_ownedEntity  = null;

        bool IComponent.OnInit(IEntity entity)
        {
            m_ownedEntity = entity;
            return true;
        }

        abstract public bool OnStart();

        bool IComponent.OnEnd()
        {
            m_ownedEntity = null;
            return true;
        }
    }
}
