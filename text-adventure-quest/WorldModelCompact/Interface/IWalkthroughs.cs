using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public interface IWalkthroughs
    {
        IDictionary<string, IWalkthrough> Walkthroughs { get; }
    }
}
