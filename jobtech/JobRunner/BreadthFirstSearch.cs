using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    // https://en.wikipedia.org/wiki/Breadth-first_search
    // http://www.mathcs.emory.edu/~cheung/Courses/171/Syllabus/11-Graph/bfs.html
    // http://algs4.cs.princeton.edu/41graph/BreadthFirstPaths.java.html
    // https://tutorialedge.net/breadth-first-search-with-java
    // use a queue

    class BreadthFirstSearch<T> : ISearch<T>
    {
        bool ISearch<T>.HasValue(GraphNode<T> start, T valueToFind)
        {
            int loops = 0;
            var queue = new Queue<GraphNode<T>>();
            queue.Enqueue(start);

            var visited = new HashSet<GraphNode<T>>();
            visited.Add(start);

            while (queue.Count > 0)
            {
                loops++;
                GraphNode<T> next = queue.Dequeue();
                Console.Write(next.Value.ToString() + " -> ");

                // found the target yet?
                if (next.Value.Equals(valueToFind))
                {
                    Console.WriteLine("BFS: " + valueToFind.ToString() + " @ " + loops);
                    return true;
                }

                next.Children.ToList().ForEach(n => 
                {
                    if (visited.Add(n))
                    {
                        queue.Enqueue(n);
                    }
                });
            }
            Console.WriteLine("BFS not:" + loops);
            return false;
        }

        SearchResult<T> ISearch<T>.ComputePath(GraphNode<T> start, T valueToFind)
        {
            throw new NotImplementedException();

            //var queue = new Queue<GraphNode<T>>();
            ////            var visited = new HashSet<GraphNode<T>>();

            //SearchResult<T> result = new SearchResult<T>();
            //result.found = false;
            //result.path = queue;

            //queue.Enqueue(start);
            ////            visited.Add(start);

            //while (queue.Count > 0)
            //{
            //    GraphNode<T> next = queue.Dequeue();

            //    // found the target yet?
            //    if (next.Value.Equals(valueToFind))
            //    {
            //        result.found = true;
            //        return result;
            //    }

            //    // all done with nodes and last leaf... then not in the tree
            //    if (next.Children.Count == 0)
            //    {
            //        result.found = false;
            //        return result;
            //    }
            //    else
            //    {
            //        next.Children.ToList().ForEach(n => queue.Enqueue(n));
            //    }
            //    //                visited.Add(next);
            //}

            //return result;
        }
    }
}
