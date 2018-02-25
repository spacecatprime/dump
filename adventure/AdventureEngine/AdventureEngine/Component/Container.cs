using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    /// <summary>
    // contains items
    // might have limits such as lockers, or might be a Room
    // might also have state of : locked, open, closed
    // might also have properties of : carriable, usable, 
    /// </summary>
    public class Container : AbstractComponent
    {
        public override Boolean OnStart()
        {
            throw new NotImplementedException();
        }
    }
}
