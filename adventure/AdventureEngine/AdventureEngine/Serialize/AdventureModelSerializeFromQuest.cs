using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureEngine.System;
using System.IO;

namespace AdventureEngine.Serialize
{
    class AdventureModelSerializeFromQuest : ISerializer
    {
        Boolean ISerializer.Load(StreamReader reader, AdventureModel engine)
        {
            throw new NotImplementedException();
        }

        Boolean ISerializer.Save(StreamWriter writer, AdventureModel engine)
        {
            throw new NotImplementedException();
        }
    }
}
