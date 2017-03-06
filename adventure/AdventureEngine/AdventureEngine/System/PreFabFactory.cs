using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;

namespace AdventureEngine.System
{
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
}
