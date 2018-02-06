using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logical
{
    public class User
    {
        public int m_rankPoints = 0;
        public uint m_gamesWon = 0;
        public uint m_gamesLost = 0;
    }

    public class Team
    {
        public string m_name;
        public List<User> m_members;
    }

    public abstract class AbstractGame
    {
        public string m_type = "";
        public List<Team> m_teams = new List<Team>();

        abstract public void Score(User u, int points);
    }

    public class MyGame : AbstractGame
    {
        public MyGame()
        {
            m_type = "MyGame";
        }

        private Dictionary<User, int> m_scoreMap = new Dictionary<User, int>();

        public override void Score(User u, int points)
        {
            if (m_scoreMap.ContainsKey(u))
            {
                m_scoreMap[u] += points;
            }
            else
            {
                m_scoreMap.Add(u, points);
            }
        }
    }

    public class GameAlgorithms
    {
        public class GameScore
        {
            public Team m_team = null;
            public int m_score = 0;
        }

        public Func<List<GameScore>> ScoreGame { get; set; }
        public IComparer<User> CompareUsers { get; set; }
    }

    public class MyGameAlgorithms : GameAlgorithms
    {
        public MyGameAlgorithms()
        {
            ScoreGame = DoScoreGame;
            CompareUsers = new CompareMyGameUsers();
        }

        private class CompareMyGameUsers: IComparer<User>
        {
            int IComparer<User>.Compare(User x, User y)
            {
                throw new NotImplementedException();
            }
        }

        private List<GameScore> DoScoreGame()
        {
            throw new NotImplementedException();
        }
    }

    public class GameSession
    {
        public DateTime m_whenPlayed;
        public AbstractGame m_gameInstance;

        public List<User> RankPlayers(GameAlgorithms algo)
        {
            var rankedUsers = new List<User>();
            foreach (var t in m_gameInstance.m_teams)
            {
                rankedUsers.AddRange(t.m_members);
            }
            rankedUsers.Sort(algo.CompareUsers);
            return rankedUsers;
        }

        public void Join(User u, Team t)
        {
            if (!m_gameInstance.m_teams.Contains(t))
            {
                m_gameInstance.m_teams.Add(t);
            }
            t.m_members.Add(u);
        }
    }

    class TestBasic
    {
        public TestBasic()
        {
            GameSession gs = new GameSession();
            Team red = new Team();
            red.m_name = "red";
            gs.Join(new User(), red);
            gs.Join(new User(), red);

            Team blue = new Team();
            blue.m_name = "blue";
            gs.Join(new User(), blue);
            gs.Join(new User(), blue);
        }

    }

    class MatchMaker
    {
        public MatchMaker()
        {
        }

        public bool Run(string[] args)
        {
            return false;
        }
    }
}
