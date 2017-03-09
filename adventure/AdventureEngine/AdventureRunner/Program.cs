using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionTests test = new ActionTests();
            test.TestSimple();

            PrefabTests pTests = new PrefabTests();
            pTests.RunSimpleTest();
        }
    }
}
