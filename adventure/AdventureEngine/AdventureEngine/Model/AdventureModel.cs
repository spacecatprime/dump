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
        ISerializer GetSerializer()
        {
            throw new NotImplementedException();
        }
        bool RegisterGameSystem(GameSystem gameSys)
        {
            throw new NotImplementedException();
        }
        bool UnregisterGameSystem(GameSystem gameSys)
        {
            throw new NotImplementedException();
        }
        void Tick(double delta)
        {
            throw new NotImplementedException();
        }
    }
}
