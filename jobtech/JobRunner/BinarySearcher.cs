using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    public class BinarySearcher
    {
        public class Result
        {
            public bool found = false;
            public int indexAt = -1;
        }

        /// <summary>
        /// Takes in a sorted array and a target value to find
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Result DoSearch(int[] array, int target)
        {
            Result result = new Result();
            int start = 0;
            int end = array.Length - 1;
            while (start <= end) 
            {
                int mid = (start + end) / 2;
                if (target == array[mid]) 
                {
                    result.found = true;
                    result.indexAt = mid;
                    return result;
                }
                if (target < array[mid]) 
                {
                    end = mid - 1;
                } else 
                {
                    start = mid + 1;
                }
            }
            return result;
        }
    }
}
