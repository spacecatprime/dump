using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public enum UIOption
    {
        UseGameColours,
        UseGameFont,
        OverrideForeground,
        OverrideLinkForeground,
        OverrideFontName,
        OverrideFontSize
    }

    public class MenuData
    {
        private string m_caption;
        private IDictionary<string, string> m_options;
        private bool m_allowCancel;

        public MenuData(string caption, IDictionary<string, string> options, bool allowCancel)
        {
            m_caption = caption;
            m_options = options;
            m_allowCancel = allowCancel;
        }

        public string Caption
        {
            get { return m_caption; }
        }

        public IDictionary<string, string> Options
        {
            get { return m_options; }
        }

        public bool AllowCancel
        {
            get { return m_allowCancel; }
        }
    }
}
