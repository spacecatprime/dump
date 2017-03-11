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
        // all meaningful data and logic that an Entity owns
        bool OnInit(IEntity entity); // used to signal that the component has been created
        bool OnStart(); // used to signal the component will be used in the game engine
        bool OnEnd(); // will stop being used in the game engine
    }
}
