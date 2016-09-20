using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.GameTypes
{
    public class ObjectTypeExit : ObjectTypeBase
    {
        public enum Direction
        { 
            NORTHWEST, 
            NORTH, 
            NORTHEAST, 
            WEST, 
            EAST, 
            SOUTHWEST, 
            SOUTH, 
            SOUTHEAST, 
            UP, 
            DOWN, 
            IN, 
            OUT
        }

        readonly public Direction m_direction;

        // $\WorldModelCompact\ObjectFactory.cs
        public ObjectTypeExit(Element element)
            : base(element)
        {
            var to = m_element.Fields.Get(FieldDefinitions.To.Property);
            var parent = m_element.Fields.Get("parent");
            var anonymous = m_element.Fields.Get(FieldDefinitions.Anonymous.Property);

            string alias = m_element.Fields.Get(FieldDefinitions.Alias.Property).ToString();
            m_direction = (Direction)Enum.Parse(typeof(Direction), alias, true);
        }

        public override string ToString()
        {
            return m_direction.ToString();
        }

    }
}

//[object]
//name
//elementtype
//type
//alias
//parent
//anonymous

//to


//name
//elementtype
//type
//alias
//parent
//anonymous
//to
//alt
//prefix
//suffix
//displayverbs
//visible
//scenery
//locked
//lockmessage
//lookonly
//runscript
//lightstrength
//grid_length
//grid_render
//grid_offset_x
//grid_offset_y
