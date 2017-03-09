using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    /// <summary>
    ///     // contains items
    // might have limits such as lockers, or might be a Room
    // might also have state of : locked, open, closed
    // might also have propertis of : carriable, usable, 
    /// </summary>
    public class Container : AbstractComponent<Container>
    {
        public override Boolean OnEnd(Object context)
        {
            throw new NotImplementedException();
        }

        public override Boolean OnInit(Object context)
        {
            throw new NotImplementedException();
        }

        public override Boolean OnStart(Object context)
        {
            throw new NotImplementedException();
        }
    }
}
