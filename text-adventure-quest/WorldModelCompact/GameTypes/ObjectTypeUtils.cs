using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.GameTypes
{
    public class ObjectTypeUtils
    {
        static public List<T> ConvertToObjectTypeList<T>(QuestList<string> elementList, WorldModel worldModel, Func<Element,T> factory) where T : ObjectTypeBase
        {
            List<T> newList = new List<T>();
            foreach (string elem in elementList)
            {
                newList.Add(factory(worldModel.Elements.Get(elem)));
            }
            return newList;
        }

        static public Dictionary<string,T> ConvertToObjectTypeDictionary<T>(QuestDictionary<string> dictionary, WorldModel worldModel, Func<Element, T> factory) where T : ObjectTypeBase
        {
            var dict = new Dictionary<string, T>();
            foreach (var kvp in dictionary)
            {
                T newObj = factory(worldModel.Elements.Get(kvp.Key));
                dict.Add(kvp.Key, newObj);
            }
            return dict;
        }
    }
}
