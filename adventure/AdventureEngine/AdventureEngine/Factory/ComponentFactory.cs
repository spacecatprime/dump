using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;

namespace AdventureEngine.Factory
{
    public class ComponentFactory
    {
        private Dictionary<string, Func<IComponent>> m_componentFunctionMap = new Dictionary<string, Func<IComponent>>();

        public bool RegisterComponentFactory(Func<IComponent> func)
        {
            IComponent comp = func();
            if (m_componentFunctionMap.ContainsKey(comp.TypeId))
            {
                return false;
            }
            m_componentFunctionMap.Add(comp.TypeId, func);
            return true;
        }
    }

    public class FactoryTemplate<TOutput, TContext, TKey>
    {
        private Dictionary<TKey, Func<TContext, TOutput>> m_factoryMap = new Dictionary<TKey, Func<TContext, TOutput>>();

        public bool ReigsterFactoryMethod(Func<TContext, TOutput> factoryFunc, Func<TKey> keyFunc)
        {
            TKey key = keyFunc();
            if (m_factoryMap.ContainsKey(key))
            {
                return false;
            }
            m_factoryMap.Add(key, factoryFunc);
            return true;
        }

        public TOutput Create(TKey key, TContext input)
        {
            if (m_factoryMap.ContainsKey(key))
            {
                return m_factoryMap[key](input);
            }
            return default(TOutput);
        }
    }
}
