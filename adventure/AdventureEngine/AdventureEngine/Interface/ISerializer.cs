using AdventureEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Interface
{
    interface ISerializer
    {
        // a generic way to serialize game state by memory, file, or network if wanted
        bool Load(StreamReader reader, AdventureModel engine);
        bool Save(StreamWriter writer, AdventureModel engine);
    }
}
