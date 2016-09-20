using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextAdventures.Quest.GameTypes;

namespace TextAdventures.Quest
{
    /// <summary>
    /// allows an controller to handle game state changes
    /// </summary>
    public interface IGameController
    {
        void UpdateExitLinks(List<ObjectTypeExit> exits);

        void UpdateCommandLinks(List<ObjectTypeCommand> commands);
    }
}
