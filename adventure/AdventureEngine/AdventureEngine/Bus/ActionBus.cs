using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Bus
{
    /// <summary>
    /// generic action list is a delegate list
    /// </summary>
    public class ActionList : List<Delegate>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ActionBus
    {
        protected class ActionMap : Dictionary<Type, ActionList>
        {
        }

        private ActionMap m_actionMap = new ActionMap();

        public void Subscribe<T, TInput>(Action<TInput> action)
        {
            // TODO make some attributes to filter action bus types such as MaxSubsubscribers

            Type type = typeof(T);
            if (m_actionMap.ContainsKey(type))
            {
                m_actionMap[type].Add(action);
            }
            ActionList actionList = new ActionList();
            actionList.Add(action);
            m_actionMap.Add(type, actionList);
        }

        public void Signal<T, TInput>(TInput signalValue)
        {
            ActionList actions;
            if (m_actionMap.TryGetValue(typeof(T), out actions))
            {
                actions.ForEach(a => a.DynamicInvoke(signalValue));
            }
        }
    }

}