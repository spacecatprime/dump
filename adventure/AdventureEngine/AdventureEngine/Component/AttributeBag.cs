using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    /// <summary>
    /// a set of named values
    /// </summary>
    public class AttributeBag : AbstractComponent 
    {
        private Dictionary<string, object> m_mappedValues = new Dictionary<string, object>();
        private string m_name;

        public override Boolean OnStart()
        {
            return m_ownedEntity != null;
        }

        public string Name
        {
            get { return m_name; }
        }

        public object GetValue(string name)
        {
            object v;
            m_mappedValues.TryGetValue(name.ToLower(), out v);
            return v;
        }

        public object SetValue(string name, object value)
        {
            object v;
            m_mappedValues.TryGetValue(name.ToLower(), out v);
            m_mappedValues[name] = value;
            return v;
        }
    }
}
