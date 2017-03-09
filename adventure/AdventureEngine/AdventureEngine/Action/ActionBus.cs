using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Action
{
    public class ActionBus
    {
        private Dictionary<Type, List<Delegate>> m_actionMap = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<T> action)
        {
            // TODO make some attributes to filter action bus types such as MaxSubsubscribers

            Type type = typeof(T);
            if (m_actionMap.ContainsKey(type))
            {
                m_actionMap[type].Add(action);
            }
            List<Delegate> actionList = new List<Delegate>();
            actionList.Add(action);
            m_actionMap.Add(type, actionList);
        }

        public void Signal<T>(T signalValue)
        {
            List<Delegate> actions;
            if (m_actionMap.TryGetValue(typeof(T), out actions))
            {
                actions.ForEach(a => a.DynamicInvoke(signalValue));
            }
        }
    }
}