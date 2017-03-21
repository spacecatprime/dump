using AdventureEngine.Bus;
using System;

namespace AdventureRunner
{
    public class ActionTests
    {
        public class ActionType
        {
            public int foo  = 1;
        }

        private void Callback(ActionType obj)
        {
            Console.WriteLine("Callback " + obj.foo.ToString());
        }

        public void TestSimple()
        {
            ActionBus bus = new ActionBus();
            bus.Subscribe<ActionType, ActionType>(new Action<ActionType>(Callback));
            bus.Signal<ActionType, ActionType>(new ActionType());
        }
    }
}
