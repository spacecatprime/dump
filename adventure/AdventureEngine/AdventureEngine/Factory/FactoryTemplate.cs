using AdventureEngine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Factory
{
    public class FactoryTemplate<TOutput, TContext, TKey>
    {
        private Dictionary<TKey, Func<GameEngine, TContext, TOutput>> m_factoryMap = new Dictionary<TKey, Func<GameEngine, TContext, TOutput>>();
        private GameEngine m_gameEngine;

        public FactoryTemplate(GameEngine gameEngine)
        {
            m_gameEngine = gameEngine;
        }

        public bool ReigsterFactoryMethod(Func<GameEngine, TContext, TOutput> factoryFunc, Func<TKey> keyFunc)
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
                return m_factoryMap[key](m_gameEngine, input);
            }
            return default(TOutput);
        }

    }
}
