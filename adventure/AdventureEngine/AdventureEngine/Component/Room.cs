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
            get { return typeof(Room).ToString(); }
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
