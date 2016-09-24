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

        public static void PrintArray<T>( T[] array, string prefix)
        {
            Console.Write(prefix);
            array.ToList().ForEach(i => Console.Write(i + ","));
            Console.WriteLine();
        }

        public static void QuickSort(string[] args)
        {
            QuickSort sorter = new QuickSort();

            int[] dataOne = new int[] { 1, 2, 5, 6, 9, 3 };
            PrintArray(dataOne, "Unsorted: ");
            sorter.DoWork(dataOne, 0, dataOne.Length - 1);
            PrintArray(dataOne, "Sorted: ");

            BinarySearcher.Result ret1 = BinarySearcher.DoSearch(dataOne, 2);
            System.Diagnostics.Debug.Assert(ret1.found);
            BinarySearcher.Result ret2 = BinarySearcher.DoSearch(dataOne, 10);
            System.Diagnostics.Debug.Assert(false == ret2.found);

            Random r = new Random();
            int[] radomData = new int[] { r.Next() % 100, r.Next() % 100, r.Next() % 100, r.Next(), r.Next() % 100, r.Next() % 100, r.Next() % 100, r.Next() % 100 };
            PrintArray(radomData, "Unsorted random: ");
            sorter.DoWork(radomData, 0, radomData.Length - 1);
            PrintArray(radomData, "Sorted random: ");
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
            Tests.QuickSort(args);
            
            Console.ReadLine();
        }

    }
}

