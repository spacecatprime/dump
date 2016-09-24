using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    class Sudoku
    {
        // https://en.wikipedia.org/wiki/Sudoku
        public void DoWork(string[] args)
        {
#if false
            List<int> line0 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line3 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line4 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line5 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line6 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line7 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> line8 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
#else
            List<int> line0 = new List<int> { 5, 3, 4, 6, 7, 8, 9, 1, 2 };
            List<int> line1 = new List<int> { 6, 7, 2, 1, 9, 5, 3, 4, 8 };
            List<int> line2 = new List<int> { 1, 9, 8, 3, 4, 2, 5, 6, 7 };
            List<int> line3 = new List<int> { 8, 5, 9, 7, 6, 1, 4, 2, 3 };
            List<int> line4 = new List<int> { 4, 2, 6, 8, 5, 3, 7, 9, 1 };
            List<int> line5 = new List<int> { 7, 1, 3, 9, 2, 4, 8, 5, 6 };
            List<int> line6 = new List<int> { 9, 6, 1, 5, 3, 7, 2, 8, 4 };
            List<int> line7 = new List<int> { 2, 8, 7, 4, 1, 9, 6, 3, 5 };
            List<int> line8 = new List<int> { 3, 4, 5, 2, 8, 6, 1, 7, 9 };
#endif
            int[][] values = new int[][] { line0.ToArray(), line1.ToArray(), line2.ToArray(), line3.ToArray(), line4.ToArray(), line5.ToArray(), line6.ToArray(), line7.ToArray(), line8.ToArray() };

            System.Console.WriteLine("Is Solved = " + IsSolved(values).ToString());
        }

        public bool IsSolved(int[][] values)
        {
            // sanitize
            if (values.Length != 9)
            {
                return false;
            }
            for (int i = 0; i < 9; ++i)
            {
                for (int k = 0; k < 9; ++k)
                {
                    int v = values[i][k];
                    if (v < 1 || v > 9)
                    {
                        return false;
                    }
                }
            }


            // check rows
            for (int i = 0; i < 9; ++i)
            {
                int[] row = values[i];
                ISet<int> set = new HashSet<int>();
                for (int k = 0; k < 9; ++k)
                {
                    int v = row[k];
                    if (set.Add(v) == false)
                    {
                        return false;
                    }
                }
            }

            // check columns
            for (int i = 0; i < 9; ++i)
            {
                ISet<int> set = new HashSet<int>();
                for (int k = 0; k < 9; ++k)
                {
                    int v = values[k][i];
                    if (set.Add(v) == false)
                    {
                        return false;
                    }
                }
            }

            // check grids
            Dictionary<int, HashSet<int>> grids = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < 9; ++i)
            {
                for (int k = 0; k < 9; ++k)
                {
                    int gridX = i / 3;
                    int gridY = k / 3;
                    int key = (4 << gridX) | gridY;
                    if (grids.ContainsKey(key) == false)
                    {
                        grids.Add(key, new HashSet<int>());
                    }
                    int v = values[i][k];
                    if (grids[key].Add(v) == false)
                    {
                        return false;
                    }
                }
            }

            // all checked out
            return true;
        }
    }
}
