using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;
using AdventureEngine.System;

namespace AdventureEngine.Factory
{
    public struct PreFabDesc
    {
    }

    public class PreFabFactory : FactoryTemplate<IPreFab<PreFabDesc>, PreFabDesc, Type>
    {
        public PreFabFactory(GameEngine ge)
            : base(ge)
        {
  
        }

        public void RegisterPreFabFactory<TOutput>(Func<GameEngine, PreFabDesc, IPreFab<PreFabDesc>> func)  where TOutput : class, IPreFab<PreFabDesc>
        {
            base.ReigsterFactoryMethod(func, () => typeof(TOutput));
        }
    }
}
