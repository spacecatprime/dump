using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Design an on-line a team fight game match system (similar to Dota, LOL, etc.):
    NxN game (start with 5v5. if candidate's design can easily extend to NxN, good sign for maintainable code)
    single player or a group of players can make request to system to start a game. (Hope candidate can ask this: If make request as a group of people, they must in the same team in the game).
    system select 2N people to build two teams which should have similar experience to start a game (game score for each player should a good start)
    based on player performance in this game update each player game score
    make match algorithm easy to change 
*/

namespace logical
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                MatchMaker mm = new MatchMaker();
                mm.Run(args);
            }

            {
                MatchBase mb = new MatchBase();
                mb.Run(args);
            }
        }
    }
}
