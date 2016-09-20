using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.GameTypes
{
    public class ObjectTypeObject : ObjectTypeBase
    {
        public ObjectTypeObject(Element element)
            : base(element)
        {
        }

        public override string ToString()
        {
            return m_element.ToString();
        }
    }
}
