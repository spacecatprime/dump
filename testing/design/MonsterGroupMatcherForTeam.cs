using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace design
{
    /// <summary>
    /// Design a matching algorithm that creates a group of monsters that matches well with a hero for a fighting match.
    /// 
    /// Elements needed:
    /// 
    /// Expand:
    /// use more options: henchmen, equipment, powers
    /// quirks: traits that work well or against an entity (fire vs water)
    /// monsters vs monsters
    /// expand factions: monster, NPC, hero, neutral
    /// 
    /// </summary>
    class MonsterGroupMatcherForTeam
    {
        //
        // interface
        //
        interface IRating
        {
            int GetThreat(IRating toOther);
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

            public IRating Rating
            {
                get { return m_rating; }
            }

            public IFaction Faction
            {
                get { return m_faction; }
            }

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

            public int GetThreat(IRating other)
            {
                return m_rating; 
            }

            public override string ToString()
            {
                return m_rating.ToString();
            }
        }

        class GroupRating : IRating
        {
            List<IEntityTemplate> m_EntityTemplates = new List<IEntityTemplate>();

            public GroupRating(List<IEntityTemplate> templates)
            {
                m_EntityTemplates = templates;
            }

            public int GetThreat(IRating other)
            {
                return m_EntityTemplates.Sum(e => e.Rating.GetThreat(other));
            }

            public override string ToString()
            {
                return GetThreat(null).ToString();
            }
        }

        class EntiyGroupTemplate : EntityTemplate, IRating
        {
            public EntiyGroupTemplate(List<IEntityTemplate> entityList) 
                : base(entityList[0].Faction, new GroupRating(entityList))
            {
            }

            public int GetThreat(IRating other)
            {
                return (Rating as GroupRating).GetThreat(other);
            }
        }

        //
        // the matcher
        //
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

            void IEntityManager.Add(IEntityTemplate e)
            {
                m_enityList.Add(e);
            }

            void IEntityManager.Remove(IEntityTemplate e)
            {
                m_enityList.Remove(e);
            }

            IEnumerable<IEntityTemplate> IEntityManager.EnityMatchRating(IEntityTemplate forThis, int minRating, int maxRating)
            {
                return m_enityList.Where((e) =>
                {
                    if (e.Faction.IsFriend(forThis.Faction))
                    {
                        return false;
                    }
                    int threatForThis = forThis.Rating.GetThreat(e.Rating);
                    return (threatForThis >= minRating && threatForThis <= maxRating);
                })
                .AsEnumerable<IEntityTemplate>();
            }

            List<IEntityTemplate> IEnemyGroupMaker.MakeEnemyGroup(IEntityTemplate forThis)
            {
                int baseThreat = forThis.Rating.GetThreat(null);

                var matchesWithRating = (this as IEntityManager).EnityMatchRating(
                    forThis,
                    baseThreat - m_ratingRange,
                    baseThreat + m_ratingRange);

                int maxThreatRating = baseThreat + m_ratingRange;
                int threatSoFar = 0;

                var targetEnemies = new List<IEntityTemplate>(matchesWithRating);
                var group = new List<IEntityTemplate>();
                do
                {
                    int idx = m_random.Next() % targetEnemies.Count;
                    int thisThreat = targetEnemies[idx].Rating.GetThreat(forThis.Rating);
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
                while (threatSoFar < maxThreatRating && targetEnemies.Count > 0);
                return group;
            }
        }

        //
        // tests
        //
        private bool RunSimpleTestSingleHero()
        {
            MonsterMatcher matcher = new MonsterMatcher(1, new Random(DateTime.Now.Millisecond));
            IEntityManager entityMgr = (IEntityManager)matcher;
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(8)));

            var hero = new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(2));

            IEnemyGroupMaker maker = (IEnemyGroupMaker)matcher;
            var group = maker.MakeEnemyGroup(hero);

            return group.Count > 0;
        }

        private bool RunSimpleTestSingleHeroGroup()
        {
            MonsterMatcher matcher = new MonsterMatcher(1, new Random(DateTime.Now.Millisecond));
            IEntityManager entityMgr = (IEntityManager)matcher;
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4)));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(8)));

            var heroList = new List<IEntityTemplate>()
            {
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(2)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(3)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(4))
            };
            var heroGroup = new EntiyGroupTemplate(heroList);

            IEnemyGroupMaker maker = (IEnemyGroupMaker)matcher;
            EntiyGroupTemplate mob = new EntiyGroupTemplate(maker.MakeEnemyGroup(heroGroup));

            int mobRating = mob.GetThreat(heroGroup);
            int heroRatingMax = heroGroup.GetThreat(mob) + 1;
            return mobRating <= heroRatingMax;
        }

        private bool RunSimpleTestGroupToGroup()
        {
            int ratingRange = 1;
            MonsterMatcher matcher = new MonsterMatcher(ratingRange, new Random(DateTime.Now.Millisecond));
            ((IEntityManager)matcher).Add(new EntiyGroupTemplate(new List<IEntityTemplate>()
            {
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1))
            }));
            ((IEntityManager)matcher).Add(new EntiyGroupTemplate(new List<IEntityTemplate>()
            {
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4))
            }));

            var heroList = new List<IEntityTemplate>()
            {
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(5)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(5)),
                new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(6))
            };
            var heroGroup = new EntiyGroupTemplate(heroList);

            var group = ((IEnemyGroupMaker)matcher).MakeEnemyGroup(heroGroup);
            int mobRating = group.Sum(m => m.Rating.GetThreat(null));

            return mobRating <= (heroGroup.GetThreat(null) + ratingRange);
        }

        public bool RunTests()
        {
            return 
                RunSimpleTestSingleHero() 
                && RunSimpleTestSingleHeroGroup()
                && RunSimpleTestGroupToGroup()
                ;
        }
    }
}
