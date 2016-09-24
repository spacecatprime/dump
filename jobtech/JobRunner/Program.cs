using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    public class Tests
    {
        public static void StringReverse(string[] args)
        {
            string foobar = JobRunner.StringReverse.DoWork("raboof".ToArray());
            System.Diagnostics.Debug.Assert(foobar == "foobar");
        }

        public static void QuickSort()
        { 
        }

        public static void Sudoku(string[] args)
        {
            Sudoku sudoku = new Sudoku();
            sudoku.DoWork(args);
        }

        public static void BreadthFirstSearch()
        {
            Graph<string> graph = new Graph<string>(10, t => t.ToString());
            graph.BuildTree(2);
            Console.WriteLine("BFS tree");
            //          0
            //      1        2
            //   3      4   5   6
            // 7  8   9

            ISearch<string> search = new BreadthFirstSearch<string>();
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "9"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "3"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "7"));
            System.Diagnostics.Debug.Assert(false == search.HasValue(graph.Nodes[0], "10"));

            //  0 1 2 3
            //  4 5 6 7
            //  8 9 - -
            graph.BuildGrid(4, 4);
            Console.WriteLine("BFS grid");
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "9"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "3"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "7"));
            System.Diagnostics.Debug.Assert(false == search.HasValue(graph.Nodes[0], "10"));
        }

        public static void DepthFirstSearch()
        {
            Graph<string> graph = new Graph<string>(10, t => t.ToString());
            graph.BuildTree(2);
            Console.WriteLine("DFS tree");
            //          0
            //      1        2
            //   3      4   5   6
            // 7  8   9

            ISearch<string> search = new DepthFirstSearch<string>();
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "9"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "3"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "7"));
            System.Diagnostics.Debug.Assert(false == search.HasValue(graph.Nodes[0], "10"));

            //  0 1 2 3
            //  4 5 6 7
            //  8 9 - -
            graph.BuildGrid(4, 4);
            Console.WriteLine("DFS grid");
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "9"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "3"));
            System.Diagnostics.Debug.Assert(search.HasValue(graph.Nodes[0], "7"));
            System.Diagnostics.Debug.Assert(false == search.HasValue(graph.Nodes[0], "10"));
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Tests.Sudoku(args);
            Tests.StringReverse(args);
            Tests.BreadthFirstSearch();
            Tests.DepthFirstSearch();
            
            Console.ReadLine();
        }

    }
}

