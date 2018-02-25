using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logical
{
    namespace Game
    {
        public interface Matcher
        {
            int MatchScore(Player p);
        }

        public class Type
        {
            public Guid m_id = Guid.NewGuid();
        }

        public abstract class Session : Matcher
        {
            public Type m_gameType;
            public List<Player> m_players = new List<Player>();
            public Player m_host;

            public abstract int MatchScore(Player p);
        }

        public class Player
        {
            public Guid m_id = Guid.NewGuid();
            public int m_skill;
        }

        public class MyGameSessionType : Session
        {
            int deviation = 100;

            public override int MatchScore(Player joiner)
            {
                int bestScore = int.MaxValue;
                foreach (var p in m_players)
                {
                    int score = p.m_skill - joiner.m_skill;
                    if (score > deviation || score < -deviation)
                    {
                        continue;
                    }
                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                }
                return bestScore;
            }
        }
    }

    public class SessionMatcher
    {
        private List<Game.Session> m_matchingSessions = new List<Game.Session>();

        public Game.Session HostGameSession(Game.Player host, Game.Type gameType, Func<Game.Session> gameSessionFactory)
        {
            // TODO: make sure host is not already in another game session
            var gs = gameSessionFactory();
            gs.m_gameType = gameType;
            gs.m_host = host;
            gs.m_players.Add(host);

            m_matchingSessions.Add(gs);
            return gs;
        }

        public Game.Session JoinGameSession(Game.Player joiner, Game.Type gameType, int target)
        {
            var gamesOfType = m_matchingSessions.Where((gs) => { return gs.m_gameType == gameType; });

            int bestTarget = int.MaxValue;
            Game.Session fallbackSession = null;

            var matched = gamesOfType.Where((gs) => 
            {
                int score = gs.MatchScore(joiner);
                if (score <= target)
                {
                    return true;
                }
                else if (score < bestTarget)
                {
                    bestTarget = score;
                    fallbackSession = gs;
                }
                return false;
            });

            if (matched.Count() >= 0)
            {
                return matched.ElementAt(0);
            }
            return fallbackSession;
        }
    }
}
