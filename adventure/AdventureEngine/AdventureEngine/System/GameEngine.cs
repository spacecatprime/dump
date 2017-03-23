using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.System
{
    /// <summary>
    /// contains the states of the game for the model
    /// holds the GameSystem instances
    /// </summary>
    public class GameEngine
    {
        public virtual bool RegisterGameSystem(GameSystem gameSys)
        {
            throw new NotImplementedException();
        }
        public virtual bool UnregisterGameSystem(GameSystem gameSys)
        {
            throw new NotImplementedException();
        }
    }
}
