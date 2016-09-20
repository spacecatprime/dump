using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public delegate void UpdateListHandler(ListType listType, List<ListData> items);

    public enum ListType
    {
        InventoryList,
        ExitsList,
        ObjectsList
    }

    public class ListData
    {
        private string m_text;
        private IEnumerable<string> m_verbs;
        private string m_elementId;
        private string m_elementName;

        public ListData(string text, IEnumerable<string> verbs)
            : this(text, verbs, null, text)
        {
        }

        public ListData(string text, IEnumerable<string> verbs, string elementId, string elementName)
        {
            m_text = text;
            m_verbs = verbs;
            m_elementId = elementId;
            m_elementName = elementName;
        }

        public string Text
        {
            get { return m_text; }
        }

        public IEnumerable<string> Verbs
        {
            get { return m_verbs; }
        }

        public string ElementId
        {
            get { return m_elementId; }
        }

        public string ElementName
        {
            get { return m_elementName; }
        }
    }
}
