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

        public virtual bool CanHandle(string tokens)
        {
            throw new NotImplementedException();
        }
    }
}