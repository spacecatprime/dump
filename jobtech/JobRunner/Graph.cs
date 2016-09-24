using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    // http://algs4.cs.princeton.edu/41graph/Graph.java.html

    public class Graph<T>
    {
        public List<GraphNode<T>> Nodes = new List<GraphNode<T>>();

        public Graph(int size, Func<int, T> makeValue)
        {
            for (int i = 0; i < size; ++i)
            {
                Nodes.Add(new GraphNode<T>(makeValue(i)));
            }
        }

        public void BuildTree(int numKidsPerNode)
        {
            Nodes.ToList().ForEach(n => n.Children.Clear());

            int next = 1;
            for (int k = 0; k < Nodes.Count; ++k)
            {
                for (int j = 0; j < numKidsPerNode; ++j)
                {
                    Nodes[k].AddChild(Nodes[next]);
                    next++;

                    // ran out of kids?
                    if (next >= Nodes.Count)
                    {
                        return;
                    }
                }
            }
        }

        private void SetGridNeighbor(GraphNode<T> node, GraphNode<T>[,] grid, int col, int row)
        {
            if (node == null)
            {
                return;
            }
            if (col < 0 || col >= grid.GetLength(0))
            {
                return;
            }
            if (row < 0 || row >= grid.GetLength(1))
            {
                return;
            }
            GraphNode<T> target = grid[col,row];
            if (target == null)
            {
                return;
            }
            node.Children.Add(target);
        }

        public void BuildGrid(int columns, int rows)
        {
            Nodes.ToList().ForEach(n => n.Children.Clear());

            int total = 0;
            GraphNode<T>[,] grid = new GraphNode<T>[columns,rows];

            for (int c = 0; c < columns; ++c)
            {
                for (int r = 0; r < rows; ++r)
                {
                    if (total >= Nodes.Count)
                    {
                        grid[c, r] = null;
                    }
                    else
                    {
                        grid[c, r] = Nodes[total];
                    }
                    total++;
                }
            }

            for (int c = 0; c < columns; ++c)
            {
                for (int r = 0; r < rows; ++r)
                {
                    SetGridNeighbor(grid[c, r], grid, c - 1, r);
                    SetGridNeighbor(grid[c, r], grid, c + 1, r);
                    SetGridNeighbor(grid[c, r], grid, c, r + 1);
                    SetGridNeighbor(grid[c, r], grid, c, r - 1);
                }
            }
        }
    }
}
