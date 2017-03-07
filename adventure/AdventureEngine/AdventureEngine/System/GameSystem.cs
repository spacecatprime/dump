using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.System
{
    /// <summary>
    /// the logic parts of the game engine
    /// </summary>
    public abstract class GameSystem
    {
        public virtual bool CanHandle(string tokens)
        {
            throw new NotImplementedException();
        }
    }
}