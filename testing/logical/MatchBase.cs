using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logical
{
    public class MatchUser
    {
        public Guid m_id = Guid.NewGuid();
        public long m_rankPoints = 0;
        public long m_gamesWon = 0;
        public long m_gamesLost = 0;

        public MatchUser() { }
        public MatchUser(long pnts, long won, long lost)
        {
            m_rankPoints = pnts;
            m_gamesWon = won;
            m_gamesLost = lost;
        }

        public override string ToString()
        {
            return string.Format("{3}: {0}, {1}, {2}", m_rankPoints, m_gamesWon, m_gamesLost, m_id.ToString());
        }
    }

    public class MatchResult
    {
        public List<MatchUser> m_items = new List<MatchUser>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var m in m_items)
            {
                sb.AppendLine(m.ToString());
            }
            return sb.ToString();
        }
    }

    // match algorithm 
    public interface IMatchAlgorithm
    {
        MatchResult FindMatchResult(List<MatchUser> seekerList, int maxMatches);
        int CompareUsers(MatchUser lhs, MatchUser rhs);
    }

    public class MatchManager
    {
        public HashSet<MatchUser> m_globalUserSet = new HashSet<MatchUser>();
        public List<MatchUser> m_seekingUsers = new List<MatchUser>();

        public void AddSeeker(MatchUser u)
        {
            if (!m_globalUserSet.Contains(u))
            {
                m_globalUserSet.Add(u);
            }
            m_seekingUsers.Add(u);
        }

        public void RemoveSeeker(MatchUser u)
        {
            m_seekingUsers.Remove(u);
        }

        public MatchResult GetMatchResult(IMatchAlgorithm matcher, int maxMatches)
        {
            return matcher.FindMatchResult(m_seekingUsers, maxMatches);
        }
    }

    public class MySingleUserMatcher : IMatchAlgorithm
    {
        MatchUser m_target;
        int m_deviation;

        public MySingleUserMatcher(MatchUser t, int dev)
        {
            m_target = t;
            m_deviation = dev;
        }

        public int CompareUsers(MatchUser lhs, MatchUser rhs)
        {
            if (lhs.m_rankPoints > rhs.m_rankPoints)
            {
                return -1;
            }
            else if (lhs.m_rankPoints < rhs.m_rankPoints)
            {
                return 1;
            }
            return 0;
        }

        private int BinarySearch(List<MatchUser> sortedList, long rank)
        {
            return sortedList.BinarySearch(
                new MatchUser(rank, 0, 0), 
                Comparer<MatchUser>.Create((x, y) => CompareUsers(x, y))
                );
        }

        private int UpperBound(List<MatchUser> sortedList, long upperLimit)
        {
            int index = BinarySearch(sortedList, upperLimit);

            if (index >= 0)
            {
                //exists
                return index;
            }
            else
            {
                // not exactly found
                int indexOfBiggerNeighbour = ~index; //bitwise complement of the return value

                if (indexOfBiggerNeighbour == sortedList.Count)
                {
                    // bigger than all elements
                    return sortedList.Count;
                }
                else if (indexOfBiggerNeighbour == 0)
                {
                    // smaller than all elements
                    return -1;
                }
                else
                {
                    // Between 2 elements
                    int indexOfSmallerNeighbour = indexOfBiggerNeighbour - 1;
                    return indexOfSmallerNeighbour;
                }
            }
        }

        public MatchResult FindMatchResult(List<MatchUser> seekerList, int maxMatches)
        {
            seekerList.Sort(CompareUsers);

            MatchResult result = new MatchResult();

            int self = BinarySearch(seekerList, m_target.m_rankPoints);
            if (self < 0 || self > seekerList.Count)
            {
                // TODO: detect out of bounds for 'self'
                return result;
            }
            int first = UpperBound(seekerList, m_target.m_rankPoints + m_deviation);
            first = (first < 0) ? 0 : first;

            int last = UpperBound(seekerList, m_target.m_rankPoints - m_deviation);
            last = (last >= 0) ? seekerList.Count -1 : last;

            for (int i = first; i < last; ++i)
            {
                // is not self
                if (seekerList[i] != m_target)
                {
                    if (!result.m_items.Contains(seekerList[i]))
                    {
                        result.m_items.Add(seekerList[i]);
                    }
                    if (result.m_items.Count >= maxMatches)
                    {
                        return result;
                    }
                }
            }

            return result;
        }
    }

    public class MatchBase
    {
        private void TestTwo()
        {
            MatchManager mm = new MatchManager();
            for (int i = 0; i < 20; ++i)
            {
                mm.AddSeeker(new MatchUser(i % 5, i % 3, i % 2));
            }
            var result = mm.GetMatchResult(new MySingleUserMatcher(mm.m_seekingUsers[4], 1), 4);
            Console.WriteLine(result);
        }

        internal void Run(string[] args)
        {
            TestTwo();
            Console.WriteLine(args);
        }
    }
}
