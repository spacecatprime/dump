using AdventureEngine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Action
{
    public class MoveAction
    {
        public enum Direction
        {
            North,
            South,
            East,
            West,
            NorthEast,
            NorthWest,
            SouthEast,
            SouthWest,
            Up,
            Down,
            In,
            Out
        }

        public Direction dir { get; private set; }
        public Room fromRoom { get; private set; }
        public Room toRoom { get; private set; }
    }
}
