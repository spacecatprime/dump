using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// http://stackoverflow.com/questions/8447/what-does-the-flags-enum-attribute-mean-in-c

namespace design
{
    [Flags]
    internal enum Slot
    {
        None = 0,
        Chest = 1,
        Legs = 2,
        Arms = 4,
        Head = 8,
        Hand = 16,
        NotInventory = 32, // can not put into a bag, either wear it or drop it
    }

    interface ItemAttribute
    {
        string Key { get; }
        object Value { get; set; }
    }

    class Item
    {
        public Dictionary<string, ItemAttribute> Attributes = new Dictionary<string, ItemAttribute>();
        public Slot Slots = Slot.None;
        public bool Equipped = false;

        public Item AddAttribute(ItemAttribute attrib)
        {
            Attributes[attrib.Key] = attrib;
            return this;
        }

        public ItemAttribute GetAttribute(string key)
        {
            ItemAttribute attrib;
            if (Attributes.TryGetValue(key, out attrib))
            {
                return attrib;
            }
            return null;
        }

        public bool HasType<T>()
        {
            foreach (var it in Attributes)
            {
                if (it.Value.GetType().Equals(typeof(T)))
                {
                    return true;
                }
            }
            return false;
        }

        public T GetAttributeByType<T>(T orDefault)
        {
            foreach (var it in Attributes)
            {
                if (it.Value.GetType().Equals(typeof(T)))
                {
                    return (T)it.Value;
                }
            }
            return orDefault;
        }

        public T GetAttributeValue<T>(string key, T orDefault)
        {
            ItemAttribute attrib = GetAttribute(key);
            if (attrib != null)
            {
                if ((typeof(T).Equals(attrib.Value.GetType())))
                {
                    return (T)attrib.Value;
                }
            }
            return orDefault;
        }
    }

    class GenericListItemAttribute<T> : ItemAttribute
    {
        protected string m_key;
        protected T m_value;

        public GenericListItemAttribute(string key, T initVal)
        {
            m_key = key;
            m_value = initVal;
        }

        string ItemAttribute.Key
        {
            get { return m_key; }
        }

        object ItemAttribute.Value
        {
            get { return m_value; }
            set { m_value = (T)value; }
        }

        public T TheValue()
        {
            return m_value;
        }
    }


    // Classes are Weapon, Armor, Useable
    enum ItemClassTrait
    {
        Weapon,
        Armor,
        Usable
    }

    class ItemClass : GenericListItemAttribute<ItemClassTrait>
    {
        public ItemClass(ItemClassTrait type) : base("class", type)
        {
        }
    }

    class EquipmentFactory
    {
        // Weapon: power, skill, hit_bonus, penalty
        public Item CreateWeapon(string name, Slot flags, uint power, uint skill, uint hitBonus, int penalty)
        {
            Item item = new Item();
            item.Slots = flags;
            item.AddAttribute(new ItemClass(ItemClassTrait.Weapon))
                .AddAttribute(new GenericListItemAttribute<string>("name", name))
                .AddAttribute(new GenericListItemAttribute<Int32>("penalty", penalty))
                .AddAttribute(new GenericListItemAttribute<UInt32>("hit_bonus", hitBonus))
                .AddAttribute(new GenericListItemAttribute<UInt32>("power", power))
                .AddAttribute(new GenericListItemAttribute<UInt32>("skill", skill))
                ;
            return item;
        }

        // Armor: defense, skill, penalty
        public Item CreateArmor(string name, Slot flags, uint defense, uint skill, int penalty)
        {
            Item item = new Item();
            item.Slots = flags;
            item.AddAttribute(new ItemClass(ItemClassTrait.Armor))
                .AddAttribute(new GenericListItemAttribute<string>("name", name))
                .AddAttribute(new GenericListItemAttribute<UInt32>("defense", defense))
                .AddAttribute(new GenericListItemAttribute<UInt32>("skill", skill))
                .AddAttribute(new GenericListItemAttribute<Int32>("penalty", penalty))
                ;
            return item;
        }

        // Usable: use_count, trait, modification, turn_count
        public Item CreateUsable(string name, Slot flags, uint use_count, string trait, int modification, uint turn_count)
        {
            Item item = new Item();
            item.Slots = flags;
            item.AddAttribute(new ItemClass(ItemClassTrait.Usable))
                .AddAttribute(new GenericListItemAttribute<string>("name", name))
                .AddAttribute(new GenericListItemAttribute<UInt32>("use_count", use_count))
                .AddAttribute(new GenericListItemAttribute<string>("trait", trait))
                .AddAttribute(new GenericListItemAttribute<Int32>("modification", modification))
                .AddAttribute(new GenericListItemAttribute<UInt32>("turn_count", turn_count))
                ;
            return item;
        }
    }

    class Inventory
    {
        public List<Item> m_items = new List<Item>();

        public long SumWeaponPower()
        {
            return m_items
                .Where(i => i.Equipped && i.HasType<ItemClass>())
                .Where(i => i.GetAttributeByType<ItemClass>(null).TheValue() == ItemClassTrait.Weapon)
                .Sum(i => i.GetAttributeValue<UInt32>("power", 0));
        }
    }

    class Trait
    {
    }

    class Avatar
    {

    }

    public class EquipmentDesign
    {
        public bool Run()
        {
            EquipmentFactory em = new EquipmentFactory();
            var dagger = em.CreateWeapon("dagger", Slot.Hand, 1, 0, 1, -4);
            dagger.Equipped = true;

            var plate = em.CreateArmor("plate", Slot.Chest | Slot.NotInventory, 10, 10, -10);
            var clazzPlate = plate.GetAttributeByType<ItemClass>(null);

            Inventory i = new Inventory();
            i.m_items.Add(dagger);
            i.m_items.Add(dagger);
            i.m_items.Add(plate);
            long power = i.SumWeaponPower();

            return true;
        }
    }
}
