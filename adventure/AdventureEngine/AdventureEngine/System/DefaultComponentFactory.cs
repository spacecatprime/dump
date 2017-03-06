using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Component;

namespace AdventureEngine.System
{
    public class DefaultComponentFactory
    {
        static void RegisterDefaultAdventureComponents(ComponentFactory componentFactory)
        {
            componentFactory.RegisterComponentFactory(() => new Room());
        }
    }
}
