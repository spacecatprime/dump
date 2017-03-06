using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Interface
{
    /// <summary>
    /// a component stored inside an entity
    /// </summary>
    public interface IComponent
    {
        // a static id to be used for serialization
        string TypeId { get; }

        // all meaningful data and logic that an Entity owns
        bool OnInit(object context); // used to signal that the component has been created
        bool OnStart(object context); // used to signal the component will be used in the game engine
        bool OnEnd(object context); // will stop being used in the game engine
    }
}
