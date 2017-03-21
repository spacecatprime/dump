using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Bus
{
    public class ActionList : List<Delegate>
    {
    }

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

    public class ActionBusByKey<TKey, TSignal>
    {
        private Dictionary<TKey, ActionList> m_actionRouter = new Dictionary<TKey, ActionList>();

        public void Subscribe(TKey key, Action<TSignal> action)
        {
            if (m_actionRouter.ContainsKey(key))
            {
                m_actionRouter[key].Add(action);
            }
            ActionList actionList = new ActionList();
            actionList.Add(action);
            m_actionRouter.Add(key, actionList);
        }

        public void Signal(TKey key, TSignal signalValue)
        {
            ActionList actions;
            if (m_actionRouter.TryGetValue(key, out actions))
            {
                actions.ForEach(a => a.DynamicInvoke(signalValue));
            }
        }

    }
}