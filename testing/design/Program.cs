using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace design
{
    class Program
    {
        static void Main(string[] args)
        {
            (new EquipmentDesign()).Run();
            (new SessionMatcherManager()).RunTester();
            (new MonsterGroupMatcher()).RunTests();
            (new MonsterGroupMatcherForTeam()).RunTests();
            (new MonsterGroupMatcherWithTraits()).RunTests();
        }
    }
}
