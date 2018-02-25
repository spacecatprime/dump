using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Model
{
    /// <summary>
    /// the logic parts of the game engine
    /// </summary>
    public abstract class GameSystem
    {
        protected GameEngine m_gameEngine;

        public GameSystem(GameEngine gameEngine)
        {
            m_gameEngine = gameEngine;
        }

        /// <summary>
        /// each system should get an update tick from time to time
        /// </summary>
        public abstract void Update(double delta);
    }
}