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
        string GetId();

        // a list of owned children Entity
        List<IEntity> GetChildren();

        // the parent Entity
        IEntity GetParent();

        // list of logic and data components that make this Entity
        List<IComponent> Components { get; set; }
    }
}
