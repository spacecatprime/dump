using AdventureEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Interface
{
    /// <summary>
    /// a generic way to serialize game state by memory, file, or network if wanted
    /// </summary>
    public interface ISerializer
    {
        bool Load(StreamReader reader, AdventureModel engine);
        bool Save(StreamWriter writer, AdventureModel engine);
    }
}
