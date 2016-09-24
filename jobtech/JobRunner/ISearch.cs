using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    public struct SearchResult<T>
    {
        public bool found;
        public IEnumerable<GraphNode<T>> path;
    }

    interface ISearch<T>
    {
        bool HasValue(GraphNode<T> start, T valueToFind);

        SearchResult<T> ComputePath(GraphNode<T> start, T valueToFind);
    }
}
