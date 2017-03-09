using AdventureEngine.Entity;
using AdventureEngine.Factory;
using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureRunner
{
    public class PrefabTests
    {
        public class FooEntity : AbstractEntity
        {
        }

        public class FooPreFab : IPreFab<PreFabDesc>
        {
            public IEntity Construct(PreFabDesc context)
            {
                return new FooEntity();
            }

            public static FooPreFab SpawnPreFabFoo(AdventureEngine.System.GameEngine ge, PreFabDesc desc)
            {
                return new FooPreFab();
            }
        }


        public void RunSimpleTest()
        {
            PreFabFactory factory = new PreFabFactory(new AdventureEngine.System.GameEngine());
            factory.RegisterPreFabFactory<FooPreFab>(FooPreFab.SpawnPreFabFoo);
            var f = factory.Create(typeof(FooPreFab), new PreFabDesc());
            var o = f.Construct(new PreFabDesc());
        }
    }
}
