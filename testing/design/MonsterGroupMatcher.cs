using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace design
{
    /// <summary>
    /// Design a matching algorithm that creates a group of monsters that match well with a hero for a fighting match.
    /// 
    /// Elements needed:
    /// - entity: base for monster or hero
    /// - monsters
    /// - hero
    /// - matcher
    /// - rating: skill or value or threat rating or equipment
    /// 
    /// Expand:
    /// use more options: henchmen, equipment, powers
    /// quirks: traits that work well or against an entity (fire vs water)
    /// monsters vs heroes
    /// monsters vs monsters
    /// 
    /// expand factions: monster, NPC, hero, neutral
    /// 
    /// </summary>
    class MonsterGroupMatcher
    {
        //
        // interface
        //
        interface IRating
        {
            int Threat { get; }
        }

        interface IFaction
        {
            bool IsFriend(IFaction other);
        }

        interface IEntityTemplate
        {
            IRating Rating { get; }
            IFaction Faction { get; }
        }

        interface IEntityManager
        {
            void Add(IEntityTemplate e);
            void Remove(IEntityTemplate e);
            IEnumerable<IEntityTemplate> EnityMatchRating(IEntityTemplate forThis, int minRating, int maxRating);
        }

        interface IEnemyGroupMaker
        {
            List<IEntityTemplate> MakeEnemyGroup(IEntityTemplate forThis);
        }

        //
        // class
        //
        class EntityTemplate : IEntityTemplate
        {
            public EntityTemplate(IFaction f, IRating r)
            {
                m_faction = f;
                m_rating = r;
            }

            private IFaction m_faction;
            private IRating m_rating;

            public IFaction Faction { get { return m_faction; } }
            public IRating Rating { get { return m_rating; } }

            public override string ToString()
            {
                return string.Format("{0} at {1}", m_faction.ToString(), m_rating.ToString());
            }
        }

        class EntityFaction : IFaction
        {
            public enum FactionKind
            {
                Monster,
                Hero
            };

            private FactionKind m_faction;

            public EntityFaction(FactionKind f)
            {
                m_faction = f;
            }

            public bool IsFriend(IFaction other)
            {
                if (other is EntityFaction) 
                {
                    return (other as EntityFaction).m_faction == m_faction;
                }
                return false;
            }

            public override string ToString()
            {
                return m_faction.ToString();
            }
        }

        class EntityRating : IRating
        {
            int m_rating = 0;

            public EntityRating(int r)
            {
                m_rating = r;
            }

            public int Threat
            {
                get { return m_rating; }
            }

            public override string ToString()
            {
                return m_rating.ToString();
            }
        }

        class MonsterMatcher : IEntityManager, IEnemyGroupMaker
        {
            List<IEntityTemplate> m_enityList = new List<IEntityTemplate>();
            int m_ratingRange;
            Random m_random;

            public MonsterMatcher(int ratingRange, Random random)
            {
                m_ratingRange = ratingRange;
                m_random = random;
            }

            public void Add(IEntityTemplate e)
            {
                m_enityList.Add(e);
            }

            public void Remove(IEntityTemplate e)
            {
                m_enityList.Remove(e);
            }

            public IEnumerable<IEntityTemplate> EnityMatchRating(IEntityTemplate forThis, int minRating, int maxRating)
            {
                return m_enityList.Where((e) =>
                {
                    if (e.Faction.IsFriend(forThis.Faction))
                    {
                        return false;
                    }
                    return (e.Rating.Threat >= minRating && e.Rating.Threat <= maxRating);
                })
                .AsEnumerable<IEntityTemplate>();
            }

            public List<IEntityTemplate> MakeEnemyGroup(IEntityTemplate forThis)
            {
                var matches = (this as IEntityManager).EnityMatchRating(
                    forThis, 
                    forThis.Rating.Threat - m_ratingRange, 
                    forThis.Rating.Threat + m_ratingRange);

                int maxThreatRating = forThis.Rating.Threat + m_ratingRange;
                int threatSoFar = 0;
                var group = new List<IEntityTemplate>();

                var targetEnemies = new List<IEntityTemplate>(matches);
                if (targetEnemies.Count == 0)
                {
                    return group;
                }

                do
                {
                    int idx = m_random.Next() % targetEnemies.Count;
                    int thisThreat = targetEnemies[idx].Rating.Threat + threatSoFar;
                    thisThreat = Math.Max(1, thisThreat);
                    if (thisThreat + threatSoFar <= maxThreatRating)
                    {
                        group.Add(targetEnemies[idx]);
                        threatSoFar += thisThreat;
                    }
                    else
                    {
                        // remove an enemy that will not contribute
                        targetEnemies.RemoveAt(idx);
                    }
                }
                while (threatSoFar < maxThreatRating && targetEnemies.Count != 0);
                return group;
            }
        }

        //
        // tests
        //
        private bool RunSimpleTest()
        {
            MonsterMatcher matcher = new MonsterMatcher(3, new Random(DateTime.Now.Millisecond));

            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1)));
            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)));
            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)));
            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4)));
            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(8)));
            matcher.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(-100)));

            var hero = new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(4));

            var group = matcher.MakeEnemyGroup(hero);

            foreach (var e in group)
            {
                Console.WriteLine(string.Format("{0}", e.ToString()));
            }

            return group.Count > 0;
        }

        public bool RunTests()
        {
            return RunSimpleTest();
        }
    }
}
