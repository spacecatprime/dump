using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    // https://tutorialedge.net/breadth-first-search-with-java

    public class GraphNode<T>
    {
        public HashSet<GraphNode<T>> Children { get; private set; }
        public T Value { get; private set; }

        public GraphNode(T value)
        {
            Value = value;
            Children = new HashSet<GraphNode<T>>();
        }

        public void AddChild(GraphNode<T> kid)
        {
            if (kid == this)
            {
                throw new InvalidOperationException("can not add self!");
            }
            Children.Add(kid);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} : {1}", Value.ToString() );
            foreach (var c in Children)
            {
                sb.AppendFormat("{0},", c.ToString());
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
