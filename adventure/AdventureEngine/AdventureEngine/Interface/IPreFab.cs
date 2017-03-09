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
    public interface IPreFab<TInput>
    {
        IEntity Construct(TInput context);
    }
}
