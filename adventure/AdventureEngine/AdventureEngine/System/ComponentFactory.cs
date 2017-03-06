﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.Interface;

namespace AdventureEngine.System
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
}