using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// fly
// hop
// swim
// walk/run
// paddle

// Ostrich swim, walk, run
// Dove fly, hop
// Penguin walk, swim, slide
// Duck fly, walk, paddle

namespace design
{
    // interface

    interface IEnvironment
    {
        float FrameTime { get; }
        bool WouldCollide(Position from, Position to);
    }

    interface IMoverData
    {
        Position CurrentPosition { get; set; }
        float Speed { get; set; }
    }

    interface ILocomotion
    {
        bool DoMove(MoveParams data);
    }

    // structures

    struct Position
    {
        public Position(float x, float y)
        {
            m_x = x;
            m_y = y;
        }

        float m_x;
        float m_y;
    }

    class MoveParams
    {
        public IMoverData m_moverData;
        public IEnvironment m_env;
    }

    // the simulator

    class FakeEnvironment : IEnvironment
    {
        public float m_frameTime;
        float IEnvironment.FrameTime
        {
            get { return m_frameTime; }
        }

        public bool m_fakeCollide;
        bool IEnvironment.WouldCollide(Position from, Position to)
        {
            return m_fakeCollide;
        }
    }

    class SimMoverData : IMoverData
    {
        Position m_currentPosition;
        public Position CurrentPosition
        {
            get { return m_currentPosition; }
            set { m_currentPosition = value; }
        }

        float m_speed;
        public float Speed
        {
            get { return m_speed; }
            set { m_speed = value; }
        }
    }

    class BirdSimulator
    {
        public List<Bird> m_birds = new List<Bird>();

        private bool m_debugToggle = false;

        public BirdSimulator()
        {
        }

        public void Upate(float delta)
        {
            var env = new FakeEnvironment();
            env.m_frameTime = delta;

            MoveParams mp = new MoveParams();
            mp.m_env = env;

            foreach (var b in m_birds)
            {
                m_debugToggle = !m_debugToggle;
                env.m_fakeCollide = m_debugToggle;

                b.Move(mp);
            }
        }
    }

    internal abstract class Bird
    {
        public abstract string BirdType { get; }
        public List<ILocomotion> LocomotionModes { get; set; }

        private float m_speed = 0.0f;
        private Position m_position = new Position(0.0f, 0.0f);

        internal void Move(MoveParams mp)
        {
            mp.m_moverData = new SimMoverData() { CurrentPosition = m_position, Speed = m_speed };

            foreach (var loc in LocomotionModes) 
            {
                if (loc.DoMove(mp)) 
                {
                    m_speed = mp.m_moverData.Speed;
                    m_position = mp.m_moverData.CurrentPosition;
                }
            }
        }
    }

    // the components 

    internal class LocomotionBase
    {
        public float m_acceleration = 1.0f;
        public float m_maxSpeed = 5.0f;
    }

    internal class Swim : LocomotionBase, ILocomotion
    {
        public bool DoMove(MoveParams data)
        {
            throw new NotImplementedException();
        }
    }

    internal class Walk : LocomotionBase, ILocomotion
    {
        public bool DoMove(MoveParams data)
        {
            throw new NotImplementedException();
        }
    }

    internal class Fly : LocomotionBase, ILocomotion
    {
        public bool DoMove(MoveParams data)
        {
            throw new NotImplementedException();
        }
    }

    internal class Hop : LocomotionBase, ILocomotion
    {
        public bool DoMove(MoveParams data)
        {
            throw new NotImplementedException();
        }
    }

    internal class Paddle : LocomotionBase, ILocomotion
    {
        public bool DoMove(MoveParams data)
        {
            throw new NotImplementedException();
        }
    }
}
