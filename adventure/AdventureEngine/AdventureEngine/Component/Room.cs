using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;

namespace AdventureEngine.Component
{
    public class Room : IComponent
    {
        string IComponent.TypeId
        {
            get { return "{1CF1EAE8-DC42-45DB-9047-34F8C201945D}"; }
        }

        bool IComponent.OnEnd(Object context)
        {
            throw new NotImplementedException();
        }

        bool IComponent.OnInit(Object context)
        {
            throw new NotImplementedException();
        }

        bool IComponent.OnStart(Object context)
        {
            throw new NotImplementedException();
        }
    }
}
