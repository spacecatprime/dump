using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Bus
{
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
