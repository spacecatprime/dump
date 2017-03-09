using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    /// <summary>
    /// reprsents a sentient being inside the adventure
    /// </summary>
    public class Character : AbstractComponent<Character>
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
