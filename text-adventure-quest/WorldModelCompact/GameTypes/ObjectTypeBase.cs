using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest.GameTypes
{
    public class ObjectTypeBase
    {
        protected readonly Element m_element;
        public Element Element { get { return m_element; } }

        public ObjectTypeBase(Element element)
        {
            m_element = element;
        }
    }
}
