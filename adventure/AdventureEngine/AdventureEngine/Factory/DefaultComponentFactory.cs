using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Component;

namespace AdventureEngine.Factory
{
    public class DefaultComponentFactory
    {
        public static void RegisterDefaultAdventureComponents(ComponentFactory componentFactory)
        {
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new AttributeBag(), () => typeof(AttributeBag));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Character(), () => typeof(Character));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Container(), () => typeof(Container));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Inventory(), () => typeof(Inventory));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Item(), () => typeof(Item));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Portal(), () => typeof(Portal));
            componentFactory.ReigsterFactoryMethod((ge, ctx) => new Room(), () => typeof(Room));
        }
    }
}
