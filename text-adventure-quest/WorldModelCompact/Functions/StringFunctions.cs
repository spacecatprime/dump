using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.Functions
{
    public static class StringFunctions
    {
        public static string CapFirst(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Substring(0, 1).ToUpper() + text.Substring(1);
        }

        public static string Left(string input, int length)
        {
            return input.Substring(length);
        }

        public static string Right(string input, int length)
        {
            return input.Substring(input.Length - length);
        }

        public static string Mid(string input, int start)
        {
            return input.Substring(start);
        }

        public static string Mid(string input, int start, int length)
        {
            return input.Substring(start, length);
        }

        public static string UCase(string input)
        {
            return input.ToUpper();
        }

        public static string LCase(string input)
        {
            return input.ToLower();
        }

        public static int LengthOf(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            return input.Length;
        }

        public static int Instr(string input, string search)
        {
            return input.IndexOf(search);
        }

        public static int Instr(int start, string input, string search)
        {
            return input.IndexOf(search, start);
        }

        public static bool StartsWith(string input, string search)
        {
            return input.StartsWith(search);
        }

        public static bool EndsWith(string input, string search)
        {
            return input.EndsWith(search);
        }

        public static QuestList<string> Split(string input, string splitChar)
        {
            return new QuestList<string>(input.Split(new string[] { splitChar }, StringSplitOptions.None));
        }

        public static string Join(IEnumerable<string> input, string joinChar)
        {
            return string.Join(joinChar, input.ToArray());
        }

        public static bool IsNumeric(string input)
        {
            int n;
            return int.TryParse(input, out n);
        }

        public static string Replace(string input, string oldString, string newString)
        {
            return input.Replace(oldString, newString);
        }
        
        public static string Trim(string input)
        {
            return input.Trim();
        }

        public static string LTrim(string input)
        {
            return input.Trim();
        }
        
        public static string RTrim(string input)
        {
            return input.Trim();
        }
        
        public static int Asc(string input)
        {
            return (int)input.ToCharArray()[0].GetTypeCode();
        }

        public static string Chr(int input)
        {
            return input.GetTypeCode().ToString();
        }
    }
}
