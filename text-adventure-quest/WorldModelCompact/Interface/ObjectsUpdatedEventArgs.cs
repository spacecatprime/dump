using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public class ObjectsUpdatedEventArgs : EventArgs
    {
        public string Added { get; set; }
        public string Removed { get; set; }
    }
}
