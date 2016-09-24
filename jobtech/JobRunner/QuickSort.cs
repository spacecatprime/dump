using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    // http://www.java2novice.com/java-sorting-algorithms/quick-sort/
    // http://www.programcreek.com/2012/11/quicksort-array-in-java/

    public class QuickSort
    {
        public void DoWork(int[] array, int lowIndex, int highIndex)
        {
            if (array.Length < 2)
            {
                return; // all done!
            }

            // no window to work in?
            if (lowIndex >= highIndex)
            {
                return;
            }

            // need a pivot
            int middleIndex = lowIndex + ((highIndex - lowIndex) / 2);
            int pivotValue = array[middleIndex];

            // expand value windows
            int leftIndex = lowIndex;
            int rightIndex = highIndex;
            while (leftIndex <= rightIndex)
            {
                while (array[leftIndex] < pivotValue)
                {
                    ++leftIndex;
                }
                while (array[rightIndex] > pivotValue)
                {
                    --rightIndex;
                }

                // swap time?
                if (leftIndex <= rightIndex)
                { 
                    int temp = array[leftIndex];
                    array[leftIndex] = array[rightIndex];
                    array[rightIndex] = temp;
                    ++leftIndex;
                    --rightIndex;
                }
            }

            // parsing next value windows
            if (lowIndex < rightIndex)
            {
                DoWork(array, lowIndex, rightIndex);
            }
            if (highIndex > leftIndex)
            {
                DoWork(array, leftIndex, highIndex);
            }
        }
    }
}
