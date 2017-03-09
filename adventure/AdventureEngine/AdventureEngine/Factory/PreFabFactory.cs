using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;
using AdventureEngine.System;

namespace AdventureEngine.Factory
{
#if false
    public class PreFabFactory
    {
        private Dictionary<string, Func<object, IPreFab>> m_preFabFunctionMap = new Dictionary<string, Func<object, IPreFab>>();

        public bool RegisterPreFabFactory(Func<object, IPreFab> func)
        {
            IPreFab instance = func(null);
            if (m_preFabFunctionMap.ContainsKey(instance.TypeId))
            {
                return false;
            }
            m_preFabFunctionMap.Add(instance.TypeId, func);
            return true;
        }
    }
#else
    public struct PreFabDesc
    {
    }

    public class PreFabFactory : FactoryTemplate<IPreFab<PreFabDesc>, PreFabDesc, Type> // <TOutput, TContext, TKey>
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
#endif
}
