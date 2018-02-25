using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Model
{
    /// <summary>
    /// default command system for input
    /// </summary>
    public class CommandSystem : GameSystem
    {
        public CommandSystem(GameEngine gameEngine)
            : base(gameEngine)
        {
            gameEngine.SytemActionBus.Subscribe<CommandSystem, string>(OnCommand);
        }

        public override void Update(double delta)
        {
            // TODO?
        }

        private void OnCommand(string cmd)
        {
            // TODO translate simple strings to some 
        }
    }
}
