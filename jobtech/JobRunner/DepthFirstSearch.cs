using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    // https://en.wikipedia.org/wiki/Depth-first_search
    // http://www.geeksforgeeks.org/depth-first-traversal-for-a-graph
    // http://www.programming-techniques.com/2012/07/depth-first-search-in-c-algorithm-and.html
    // https://tutorialedge.net/depth-first-search-in-java
    // use a stack

    public class DepthFirstSearch<T> : ISearch<T>
    {
        bool ISearch<T>.HasValue(GraphNode<T> start, T valueToFind)
        {
            int loops = 0;
            var stack = new Stack<GraphNode<T>>();
            stack.Push(start);

            var visited = new HashSet<GraphNode<T>>();
            visited.Add(start);

            while (stack.Count > 0)
            {
                loops++;
                var next = stack.Pop();
                Console.Write(next.Value.ToString() + " -> ");

                if (next.Value.Equals(valueToFind))
                {
                    Console.WriteLine("DFS: " + valueToFind.ToString() + " @ " + loops);
                    return true;
                }

                next.Children.ToList().ForEach(n => 
                {
                    if (visited.Add(n))
                    {
                        stack.Push(n);
                    }
                });
            }
            Console.WriteLine("DFS not: " + valueToFind.ToString() + " @ " + loops);
            return false;
        }

        SearchResult<T> ISearch<T>.ComputePath(GraphNode<T> start, T valueToFind)
        {
#if true
            throw new NotImplementedException();
#else
            var path = new Stack<GraphNode<T>>();
            path.Push(start);

            var result = new SearchResult<T>();
            result.found = false;
            result.path = path;

            var stack = new Stack<GraphNode<T>>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var next = stack.Pop();
                if (next.Value.Equals(valueToFind))
                {
                    result.found = true;
                    return result;
                }
                next.Children.ToList().ForEach(n => stack.Push(n));
            }
            return false;
#endif
        }
    }
}
