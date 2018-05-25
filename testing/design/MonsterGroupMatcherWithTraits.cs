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
    /// Adding traits matrix
    /// quirks: traits that work well or against an entity (fire vs water)
    /// 
    /// Expand:
    /// use more options: henchmen, equipment, powers
    /// monsters vs monsters
    /// expand factions: monster, NPC, hero, neutral
    /// 
    /// </summary>
    class MonsterGroupMatcherWithTraits
    {
        //
        // interface
        //
        interface ITrait
        {
            bool Matches(ITrait trait);
        }

        interface ITraitManager
        {
            void RegisterTraitRelation(ITrait thisTrait, ITrait thatTrait, int effect);
            int FindAdjustment(ITrait[] thisTrait, ITrait[] thoseTraits);
        }

        interface IRating
        {
            int GetThreat();
            ITrait[] GetTraits();
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
        class Trait : ITrait
        {
            public Trait(string n)
            {
                Name = n;
            }

            public string Name { get; private set; }

            public bool Matches(ITrait trait)
            {
                Trait other = (Trait)trait;
                return other.Name == Name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        class TraitManager : ITraitManager
        {
            Dictionary<KeyValuePair<int,int>, int> m_tableTraitRelationshipAdjustment = new Dictionary<KeyValuePair<int, int>, int>(); // hash(fromTrait, toTrait) => adjustment
            Dictionary<string, ITrait> m_allTraits = new Dictionary<string, ITrait>();

            public int FindAdjustment(ITrait[] thisTraits, ITrait[] thoseTraits)
            {
                int adjustment = 0;
                foreach (var thisT in thisTraits)
                {
                    foreach (var thatT in thoseTraits)
                    {
                        Adjustment(thisT, thatT, ref adjustment);
                    }
                }
                return adjustment;
            }

            private void Adjustment(ITrait thisTrait, ITrait thatTrait, ref int adjustment)
            {
                var key = new KeyValuePair<int, int>(thisTrait.GetHashCode(), thatTrait.GetHashCode());
                int adj = 0;
                m_tableTraitRelationshipAdjustment.TryGetValue(key, out adj);
                adjustment += adj;
            }

            public void RegisterTraitRelation(ITrait thisTrait, ITrait thatTrait, int effect)
            {
                if (!m_allTraits.ContainsKey(thisTrait.ToString()))
                {
                    m_allTraits.Add(thisTrait.ToString(), thisTrait);
                }
                if (!m_allTraits.ContainsKey(thatTrait.ToString()))
                {
                    m_allTraits.Add(thatTrait.ToString(), thatTrait);
                }

                var key = new KeyValuePair<int, int>(thisTrait.GetHashCode(), thatTrait.GetHashCode());
                m_tableTraitRelationshipAdjustment.Add(key, effect);
            }

            public ITrait GetTraitByName(string name)
            {
                ITrait t = null;
                m_allTraits.TryGetValue(name, out t);
                return t;
            }
        }

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
            ITrait[] m_traits;

            public EntityRating(int r, List<ITrait> traits)
            {
                m_rating = r;
                m_traits = traits.ToArray();
            }

            public int GetThreat()
            {
                return m_rating;
            }

            public ITrait[] GetTraits()
            {
                return m_traits;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var t in m_traits)
                {
                    sb.AppendFormat("{0}|", t.ToString());
                }
                return string.Format("{0} with {1}", m_rating, sb.ToString());
            }
        }

        class GroupRating : IRating
        {
            List<EntityTemplate> m_entiyTemplates = new List<EntityTemplate>();

            public GroupRating(List<EntityTemplate> templates)
            {
                m_entiyTemplates = templates;
            }

            public int GetThreat()
            {
                return m_entiyTemplates.Sum(e => e.Rating.GetThreat());
            }

            public ITrait[] GetTraits()
            {
                HashSet<ITrait> traits = new HashSet<ITrait>();
                foreach (var e in m_entiyTemplates)
                {
                    e.Rating.GetTraits().All(t => traits.Add(t));
                }
                return traits.ToArray();
            }

            public override string ToString()
            {
                return GetThreat().ToString();
            }
        }

        class EntiyGroupTemplate : EntityTemplate, IRating
        {
            public EntiyGroupTemplate(List<EntityTemplate> entityList) 
                : base(entityList[0].Faction, new GroupRating(entityList))
            {
            }

            public int GetThreat()
            {
                return (Rating as GroupRating).GetThreat();
            }

            public ITrait[] GetTraits()
            {
                return (Rating as GroupRating).GetTraits();
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
            TraitManager m_traitManager;

            public MonsterMatcher(int ratingRange, Random random, TraitManager traitManager)
            {
                m_ratingRange = ratingRange;
                m_random = random;
                m_traitManager = traitManager;
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
                    int threatForThis = forThis.Rating.GetThreat();
                    return (threatForThis >= minRating && threatForThis <= maxRating);
                })
                .AsEnumerable<IEntityTemplate>();
            }

            public List<IEntityTemplate> MakeEnemyGroup(IEntityTemplate forThis)
            {
                int baseThreat = forThis.Rating.GetThreat();

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
                    int thisThreat = targetEnemies[idx].Rating.GetThreat();
                    thisThreat += m_traitManager.FindAdjustment(forThis.Rating.GetTraits(), targetEnemies[idx].Rating.GetTraits());
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
                while (threatSoFar < maxThreatRating && targetEnemies.Count > 0);
                return group;
            }
        }

        //
        // tests
        //
        private TraitManager CreateTraitManager()
        {
            Trait fire = new Trait("fire");
            Trait water = new Trait("water");
            Trait earth = new Trait("earth");
            Trait air = new Trait("air");

            TraitManager traitManager = new TraitManager();
            traitManager.RegisterTraitRelation(fire, water, 5);
            traitManager.RegisterTraitRelation(fire, earth, -2);
            traitManager.RegisterTraitRelation(fire, air, 1);
            traitManager.RegisterTraitRelation(water, fire, 5);
            traitManager.RegisterTraitRelation(water, earth, 2);
            traitManager.RegisterTraitRelation(water, air, -3);
            traitManager.RegisterTraitRelation(earth, water, 2);
            traitManager.RegisterTraitRelation(earth, fire, -2);
            traitManager.RegisterTraitRelation(earth, air, 5);
            traitManager.RegisterTraitRelation(air, fire, 6);
            traitManager.RegisterTraitRelation(air, earth, -1);

            return traitManager;
        }

        private bool RunSimpleTestSingleHero()
        {
            TraitManager traitManager = CreateTraitManager();
            ITrait fire = traitManager.GetTraitByName("fire");
            ITrait water = traitManager.GetTraitByName("water");
            ITrait earth = traitManager.GetTraitByName("earth");
            ITrait air = traitManager.GetTraitByName("air");

            MonsterMatcher matcher = new MonsterMatcher(2, new Random(5), traitManager); // fire, water

            IEntityManager entityMgr = (IEntityManager)matcher;
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1, new List<ITrait>() {fire, air})));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1, new List<ITrait>() {water, fire})));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2, new List<ITrait>() {air})));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(3, new List<ITrait>() {water})));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4, new List<ITrait>() {fire})));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(5, new List<ITrait>() {earth})));

            var hero = new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(4, new List<ITrait>() {earth}));

            IEnemyGroupMaker maker = (IEnemyGroupMaker)matcher;
            var group = maker.MakeEnemyGroup(hero);

            return group.Count > 0;
        }

        private bool RunSimpleTestSingleMajorHero()
        {
            TraitManager traitManager = CreateTraitManager();
            ITrait fire = traitManager.GetTraitByName("fire");
            ITrait water = traitManager.GetTraitByName("water");
            ITrait earth = traitManager.GetTraitByName("earth");
            ITrait air = traitManager.GetTraitByName("air");
            
            MonsterMatcher matcher = new MonsterMatcher(2, new Random(1), traitManager);

            IEntityManager entityMgr = (IEntityManager)matcher;
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1, new List<ITrait>() {})));

            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1, new List<ITrait>() { water, fire })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2, new List<ITrait>() { air })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(3, new List<ITrait>() { water })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4, new List<ITrait>() { fire })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(5, new List<ITrait>() { earth, fire })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(6, new List<ITrait>() { water, water })));
            entityMgr.Add(new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(7, new List<ITrait>() { air, fire })));

            var hero = new EntityTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(10, new List<ITrait>() { water }));

            IEnemyGroupMaker maker = (IEnemyGroupMaker)matcher;
            var group = maker.MakeEnemyGroup(hero);

            return group.Count > 0;
        }

        //private bool RunSimpleTestSingleHeroGroup()
        //{
        //    MonsterMatcher matcher = new MonsterMatcher(1, new Random(DateTime.Now.Millisecond));
        //    IEntityManager entityMgr = (IEntityManager)matcher;
        //    entityMgr.Add(new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(1)));
        //    entityMgr.Add(new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)));
        //    entityMgr.Add(new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)));
        //    entityMgr.Add(new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4)));
        //    entityMgr.Add(new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(8)));

        //    var heroList = new List<EntiyTemplate>()
        //    {
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(2)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(3)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(4))
        //    };
        //    var heroGroup = new EntiyGroupTemplate(heroList);

        //    IEnemyGroupMaker maker = (IEnemyGroupMaker)matcher;
        //    EntiyGroupTemplate mob = new EntiyGroupTemplate(maker.MakeEnemyGroup(heroGroup));

        //    int mobRating = mob.GetThreat(heroGroup);
        //    int heroRatingMax = heroGroup.GetThreat(mob) + 1;
        //    return mobRating <= heroRatingMax;
        //}

        //private bool RunSimpleTestGroupToGroup()
        //{
        //    int ratingRange = 1;
        //    MonsterMatcher matcher = new MonsterMatcher(ratingRange, new Random(DateTime.Now.Millisecond));
        //    ((IEntityManager)matcher).Add(new EntiyGroupTemplate(new List<EntiyTemplate>()
        //    {
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1))
        //    }));
        //    ((IEntityManager)matcher).Add(new EntiyGroupTemplate(new List<EntiyTemplate>()
        //    {
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(1)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(2)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Monster), new EntityRating(4))
        //    }));

        //    var heroList = new List<EntiyTemplate>()
        //    {
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(5)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(5)),
        //        new EntiyTemplate(new EntityFaction(EntityFaction.FactionKind.Hero), new EntityRating(6))
        //    };
        //    var heroGroup = new EntiyGroupTemplate(heroList);

        //    var group = ((IEnemyGroupMaker)matcher).MakeEnemyGroup(heroGroup);
        //    int mobRating = group.Sum(m => m.m_rating.GetThreat(null));

        //    return mobRating <= (heroGroup.GetThreat(null) + ratingRange);
        //}

        public bool RunTests()
        {
            return RunSimpleTestSingleHero() 
                && RunSimpleTestSingleMajorHero()
                // && RunSimpleTestSingleHeroGroup()
                // && RunSimpleTestGroupToGroup()
                ;
        }
    }
}
