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
            set { m_gameEngine = value; }
        }

        public void Tick(double delta)
        {
            throw new NotImplementedException();
        }
    }
}
