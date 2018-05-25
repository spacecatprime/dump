using AdventureEngine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Interface
{
    /// <summary>
    /// A game entity
    /// </summary>
    public interface IEntity
    {
        // all simulated objects start with this one

        // a unique value per IEntity instance
        EntityId GetId();

        // a list of owned children Entity
        List<IEntity> GetChildren();

        // the parent Entity
        IEntity GetParent();

        // list of logic and data components that make this Entity
        List<IComponent> Components { get; }

        // manage a named value (if any) from any attribute bag
        object GetAttribute(string name);
        object SetAttribute(string name, object value); // returns old value
    }
}
