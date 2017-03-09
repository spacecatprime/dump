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
            AdventureEngine.Action.ActionBus bus = new AdventureEngine.Action.ActionBus();
            bus.Subscribe<ActionType>(new Action<ActionType>(Callback));
            bus.Signal<ActionType>(new ActionType());
        }
    }
}
