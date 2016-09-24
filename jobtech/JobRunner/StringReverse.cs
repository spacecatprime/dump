using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    public class StringReverse
    {
        public static string DoWork(char[] charArray)
        {
#if false
            for (int i = 0; i < charArray.Length / 2; ++i)
            {
                char t = charArray[i];
                charArray[i] = charArray[charArray.Length - (i + 1)];
                charArray[charArray.Length - (i + 1)] = t;
            }
#else
            for (int i = 0; i < charArray.Length / 2; ++i)
            {
                // x = x ^ y
                charArray[i] = (char)((int)charArray[i] ^ (int)charArray[charArray.Length - (i + 1)]);
                // y = y ^ x
                charArray[charArray.Length - (i + 1)] = (char)((int)charArray[charArray.Length - (i + 1)] ^ (int)charArray[i]);
                // x = x ^ y
                charArray[i] = (char)((int)charArray[i] ^ (int)charArray[charArray.Length - (i + 1)]);
            }
#endif
            return new String(charArray);
        }

    }
}
