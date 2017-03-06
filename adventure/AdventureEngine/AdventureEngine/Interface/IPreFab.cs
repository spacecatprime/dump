using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Interface
{
    /// <summary>
    /// contructs a preset number of Components for an Entity
    /// </summary>
    public interface IPreFab
    {
        IEntity CreateDefault(object context);

        // a unique value per PreFab type
        string TypeId { get; }
    }
}
