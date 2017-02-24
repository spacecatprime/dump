using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.GameTypes
{
    /// <summary>
    /// Represents the current state of the game 
    /// </summary>
    public class GameState
    {
        #region Properties
        public ReadOnlyCollection<ObjectTypeExit> Exits
        {
            get { return m_exits.AsReadOnly(); }
        }
        #endregion

        #region Updates
        public void UpdateExitList(List<ObjectTypeExit> exits) { }
        public void UpdateCommandList(List<ObjectTypeCommand> commandList) { }
        public void UpdateObjectList(List<ObjectTypeObject> objectList) { }
        public void UpdateGameName(string name) { }
        public void UpdateStatusText(string status) { }
        public void UpdateSpokenText(string status) { }
        #endregion

        #region Commands
        // tokens split on a space
        public void ExecuteCommand(params string[] tokens) {}
        // meant to reset the entire SIM back to beggining
        public bool Reset() { return true; }
        // TODO maybe something could be returned?
        public bool Save(string token) { return false; }
        // TODO maybe something could be sent in?
        public bool Load(string token) { return false; }
        #endregion

        #region Data
        private List<ObjectTypeExit> m_exits = new List<ObjectTypeExit>();
        #endregion
    }
}