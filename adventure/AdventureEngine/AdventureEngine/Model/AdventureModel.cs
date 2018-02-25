using AdventureEngine.Entity;
using AdventureEngine.Factory;
using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Model
{
    /// <summary>
    /// something to hold the game state and systems
    /// highest level object that should be maintained by the app
    /// </summary>
    public class AdventureModel
    {
        private GameEngine m_gameEngine = new GameEngine();
        public GameEngine TheGameEngine 
        {
            get { return m_gameEngine; }
        }

        private List<GameSystem> m_systems = new List<GameSystem>();
        public IReadOnlyList<GameSystem> GameSystems
        {
            get { return m_systems; }
        }

        private DateTime m_lastTickTime = DateTime.Now;
        public void Tick()
        {
            double delta = (DateTime.Now - m_lastTickTime).TotalSeconds;
            m_lastTickTime = DateTime.Now;
            foreach (var gs in m_systems)
            {
                gs.Update(delta);
            }
        }

        public void RegisterDefaultAdventureComponents()
        {
            DefaultComponentFactory.RegisterForDefaultAdventureEngine(TheGameEngine.ComponentFactory);
            DefaultEntityEvents.RegisterForDefaultAdventureEngine(TheGameEngine.SytemActionBus);

            // linking the systems up to the engine
            m_systems.Add(new CommandSystem(TheGameEngine));
            m_systems.Add(new MoveSystem(TheGameEngine));
        }
    }
}
