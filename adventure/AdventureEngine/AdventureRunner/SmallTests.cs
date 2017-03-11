using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventureRunner
{
    public class SmallTests
    {
        public void Run()
        {
            RunSimple();
        }

        void RunSimple()
        {
            AdventureEngine.System.GameEngine ge = new AdventureEngine.System.GameEngine();
            ge.RegisterDefaultAdventureComponents();
            var room = ge.ComponentFactory.Create(typeof(AdventureEngine.Component.Room), null);
            Assert.IsInstanceOfType(room, typeof(AdventureEngine.Component.Room));
        }
    }
}
