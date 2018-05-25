using System;
using System.Collections.Generic;
using System.Linq;

/*
 * game player matcher
 * - host adds itself to a pool
 * - 
 * 
 * Todo:
 * - host should be able to signal (ready or closed)
 * - peer should get a list of top candidates (like top 5 matches)
 * - should host require a min/max skill level?
 * - the host creates a game, but then pushes a button to automatically find a group of players
 */

namespace design
{
    namespace Game
    {
        //
        // interface
        //
        interface IPlayer
        {
            Guid Id { get; }
            int Rating { get; }
        }

        interface ISession
        {
            IPlayer[] Players { get; }
            IPlayer Host { get; }
        }

        interface IMatcher
        {
            bool DoesMatch(IPlayer p, ISession session);
            int MaxDeviation { get; }
        }
        
        //
        // class
        //
        class Session : ISession
        {
            public List<Player> m_players = new List<Player>();
            public Player m_host;

            public IPlayer[] Players
            {
                get
                {
                    return m_players.ToArray();
                }
            }

            public IPlayer Host
            {
                get
                {
                    return m_host;
                }
            }
        }

        class Player : IPlayer
        {
            private Guid m_id = Guid.Empty;
            private int m_rating;

            public Player(Guid id, int skill)
            {
                m_id = Id;
                m_rating = skill;
            }
            
            public Guid Id
            {
                get { return m_id; }
            }

            public int Rating
            {
                get { return m_rating; }
            }
        }

        class MyMatcher : IMatcher
        {
            int m_deviationAllowed;
            int m_maxDeviation = int.MinValue;

            public MyMatcher(int deviation)
            {
                m_deviationAllowed = deviation;
            }

            public int MaxDeviation
            {
                get { return m_maxDeviation; }
            }

            public bool DoesMatch(IPlayer joiner, ISession session)
            {
                foreach (var p in session.Players)
                { 
                    int score = p.Rating - joiner.Rating;
                    if (score > m_deviationAllowed || score < -m_deviationAllowed)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

    class SessionManager
    {
        private List<Game.ISession> m_activeSessions = new List<Game.ISession>();
        private Game.IMatcher m_matcher;

        public SessionManager()
        {
            m_matcher = new Game.MyMatcher(10);
        }

        public Game.Session HostGameSession(Game.Player host)
        {
            // TODO: make sure host is not already in another game session
            var gs = new Game.Session();
            gs.m_host = host;
            gs.m_players.Add(host);

            m_activeSessions.Add(gs);
            return gs;
        }

        public bool SignalHostGameSessionReady(Game.ISession session)
        {
            var host = m_activeSessions.Find(s => s == session);
            if (null == host) 
            {
                return false;
            }
            m_activeSessions.Remove(session);
            // TODO signal the host is ready & was removed
            return true;
        }

        // TODO top scores returned
        public List<Game.ISession> JoinGameSession(Game.Player joiner)
        {
            int bestTarget = int.MaxValue;
            var bestMatchingSessions = new List<Game.ISession>();

            //var matched = m_activeSessions.Where((gs) => 
            //{
            //    int score = m_matcher.MatchScore(joiner, gs);
            //    if (score < bestTarget)
            //    {
            //        bestTarget = score;
            //    }
            //    return false;
            //});

            return bestMatchingSessions;
        }
    }
    
    public class SessionMatcherManager
    {
        public bool SingleHostSingleJoiner()
        {
            Game.Player host = new Game.Player(Guid.NewGuid(), 1);
            Game.Player joiner = new Game.Player(Guid.NewGuid(), 1);
            SessionManager sm = new SessionManager();
            sm.HostGameSession(host);
            sm.JoinGameSession(joiner);
            return false;
        }

        public bool SingleHostWithJoiners()
        {
            throw new NotImplementedException();
        }

        public bool MoreHostsWithJoiners()
        {
            throw new NotImplementedException();
        }

        public bool RunTester()
        {
            return true;
        }
    }
}
