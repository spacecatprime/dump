using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Serialization
{
    public class StateReader
    {
        private System.IO.StreamReader m_stream;

        public StateReader(System.IO.StreamReader reader)
        {
            m_stream = reader;
        }
    }
}
