using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Action
{
    public class ActionList : List<Delegate>
    {
    }

    public class ActionBus
    {
        protected class ActionMap : Dictionary<Type, ActionList>
        {
        }

        private ActionMap m_ActionList = new ActionMap();

        // TODO some kind of EntityId to ActionMap

        public void Subscribe<T>(Action<T> action)
        {
            // TODO make some attributes to filter action bus types such as MaxSubsubscribers

            Type type = typeof(T);
            if (m_ActionList.ContainsKey(type))
            {
                m_ActionList[type].Add(action);
            }
            ActionList actionList = new ActionList();
            actionList.Add(action);
            m_ActionList.Add(type, actionList);
        }

        public void Signal<T>(T signalValue)
        {
            ActionList actions;
            if (m_ActionList.TryGetValue(typeof(T), out actions))
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