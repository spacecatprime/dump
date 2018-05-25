using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureEngine.Model;
using AdventureEngine.Interface;
using AdventureEngine.Component;

namespace AdventureRunner
{
    public class SmallTests
    {
        private class FakeComponent : AbstractComponent
        {
            public override bool OnStart()
            {
                System.Console.WriteLine("OnStart fake");
                return true;
            }
        }

        private class FakeEvent
        {
            public static void Foo(FakeEvent fe)
            {
                System.Console.WriteLine("Got to fake event");
            }
        }

        public void Run()
        {
            RunSimple();
            RunBuildARoom();
            RunSignalTest();
            RunCommandTest();
        }

        private void RunCommandTest()
        {
            AdventureModel model = new AdventureModel();
            model.RegisterDefaultAdventureComponents();
            model.TheGameEngine.SytemActionBus.Signal<CommandSystem,string>("Hello world");
            model.Tick();
        }

        private void RunSignalTest()
        {
            GameEngine ge = new GameEngine();
            ge.SytemActionBus.Subscribe<FakeEvent, FakeEvent>(e => FakeEvent.Foo(e));
            ge.SytemActionBus.Signal<FakeEvent, FakeEvent>(new FakeEvent());
        }

        void RunSimple()
        {
            AdventureModel model = new AdventureModel();
            model.RegisterDefaultAdventureComponents();
            var room = model.TheGameEngine.ComponentFactory.Create(typeof(Room), null);
            Assert.IsInstanceOfType(room, typeof(Room));
        }

        void RunBuildARoom()
        {
            AdventureModel model = new AdventureModel();
            model.RegisterDefaultAdventureComponents();
            GameEngine ge = model.TheGameEngine;
            IEntity e = ge.EntityFactory.CreateDefault(new object());
            e.Components.Add(ge.ComponentFactory.Create(typeof(Room), new object()));
            e.Components.Add(new FakeComponent());
            ge.AddRootEntity(e);
        }
    }
}
