using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;
using AdventureEngine.Model;

namespace AdventureEngine.Factory
{
    public class ComponentFactory : FactoryTemplate<IComponent, object, Type>
    {
        public ComponentFactory(GameEngine ge)
            : base(ge)
        {
        }
    }
}
